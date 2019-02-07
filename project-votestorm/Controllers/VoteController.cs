using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}/vote")]
    public class VoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;

        public VoteController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
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