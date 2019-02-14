using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Validators;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{pollid}")]
    [ApiController]
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
            var poll = _pollRepository.Read(pollId);
            if (poll == null)
            {
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            var votes = await _voteRepository.Get(pollId);
            var hasVoted = votes.FirstOrDefault(vote => vote.Identity == identity) != null;

            return new OkObjectResult(hasVoted);
        }

        [HttpPost("vote")]
        public async Task<IActionResult> SubmitVote([FromBody] CreatePluralityVoteRequest voteRequest,
            [FromRoute] string pollId)
        {
            var votes = await _voteRepository.Get(pollId);
            if (votes.FirstOrDefault(vote => vote.Identity == voteRequest.Identity) != null)
            {
                return new ConflictObjectResult($"That user already voted on the poll with ID {pollId}.");
            }

            var poll = await _pollRepository.Read(pollId);
            if (poll == null)
            {
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            if (!poll.IsActive)
            {
                return new StatusCodeResult((int) HttpStatusCode.Locked);
            }

            if (!CreatePluralityVoteRequestValidator.IsPollVoteValid(poll, voteRequest))
            {
                return new BadRequestObjectResult("Invalid vote for the specified poll");
            }

            try
            {
                await _voteRepository.Create(voteRequest, pollId);

                return new OkResult();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Failed to add vote to poll {pollId}", e);
                return new BadRequestObjectResult("Invalid vote object provided");
            }
        }
    }
}