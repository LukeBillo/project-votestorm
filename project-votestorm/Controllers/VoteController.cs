using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}")]
    public class VoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;

        public VoteController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHasVoted([FromQuery] string pollId, [FromQuery] string identity)
        {
            var votes = await _voteRepository.Get(pollId);
            var hasVoted = votes.First(vote => vote.Identity == identity) != null;

            return new OkObjectResult(hasVoted);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVote([FromBody] CreatePluralityVoteRequest voteRequest, [FromRoute] string pollId)
        {
            var votes = await _voteRepository.Get(pollId);

            if (votes.First(vote => vote.Identity == voteRequest.Identity) != null)
            {
                return new ConflictObjectResult($"That user already voted on the poll with ID {pollId}.");
            }

            await _voteRepository.Create(voteRequest, pollId);

            return new OkResult();
        }
    }
}