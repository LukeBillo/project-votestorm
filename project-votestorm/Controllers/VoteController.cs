using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}")]
    public class VoteController : Controller
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPollRepository _pollRepository;
        private readonly ILogger<VoteController> _logger;

        public VoteController(IVoteRepository voteRepository, IPollRepository pollRepository,
            ILogger<VoteController> logger)
        {
            _voteRepository = voteRepository;
            _pollRepository = pollRepository;
            _logger = logger;
        }

        [HttpGet("voted")]
        public async Task<IActionResult> GetHasVoted([FromRoute] string pollId, [FromQuery] string identity)
        {
            try
            {
                var votes = await _voteRepository.Get(pollId);
                var hasVoted = votes.FirstOrDefault(vote => vote.Identity == identity) != null;

                return new OkObjectResult(hasVoted);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("Failed to find poll with ID " + pollId, e);
                return new NotFoundObjectResult("No poll found with the given ID");
            }
        }

        [HttpPost("vote")]
        public async Task<IActionResult> SubmitVote([FromBody] CreatePluralityVoteRequest voteRequest,
            [FromRoute] string pollId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid vote object provided");
            }

            var votes = await _voteRepository.Get(pollId);
            if (votes.FirstOrDefault(vote => vote.Identity == voteRequest.Identity) != null)
            {
                return new ConflictObjectResult("That user already voted on the poll with ID {pollId}.");
            }

            try
            {
                var poll = await _pollRepository.Read(pollId);
                if (!poll.IsActive)
                {
                    return new StatusCodeResult(423);
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("Failed to find poll with ID " + pollId, e);
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            try
            {
                await _voteRepository.Create(voteRequest, pollId);

                return new OkResult();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("Failed to add vote to poll " + pollId, e);
                return new BadRequestObjectResult("Invalid vote object provided");
            }
        }
    }
}