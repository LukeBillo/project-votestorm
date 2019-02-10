﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Validators;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}")]
    public class VoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPollRepository _pollRepository;

        public VoteController(IVoteRepository voteRepository, IPollRepository pollRepository)
        {
            _voteRepository = voteRepository;
            _pollRepository = pollRepository;
        }

        [HttpGet("voted")]
        public async Task<IActionResult> GetHasVoted([FromRoute] string pollId, [FromQuery] string identity)
        {
            var votes = await _voteRepository.Get(pollId);
            var hasVoted = votes.FirstOrDefault(vote => vote.Identity == identity) != null;

            return new OkObjectResult(hasVoted);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> SubmitVote([FromBody] CreatePluralityVoteRequest voteRequest, [FromRoute] string pollId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            
            var votes = await _voteRepository.Get(pollId);

            if (votes.FirstOrDefault(vote => vote.Identity == voteRequest.Identity) != null)
            {
                return new ConflictObjectResult($"That user already voted on the poll with ID {pollId}.");
            }

            var poll = await _pollRepository.Read(pollId);
            if (!poll.IsActive)
            {
                return new StatusCodeResult(423);
            }

            await _voteRepository.Create(voteRequest, pollId);

            return new OkResult();
        }
    }
}