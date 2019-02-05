using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}/vote")]
    public class VoteController : Controller
    {
        private readonly VoteRepository _voteRepository;

        public VoteController(VoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPluralityVote([FromBody] CreatePluralityVoteRequest voteRequest, [FromRoute] string pollId)
        {
            await _voteRepository.Create(voteRequest, pollId);

            return new OkResult();
        }

        public async Task<IActionResult> SubmitScoringVote(/*[FromBody] createVoteRequest?*/)
        {
            throw new NotImplementedException();
        }
    }
}