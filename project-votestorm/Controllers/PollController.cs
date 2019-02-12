using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll")]
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollIdGenerator _pollIdGenerator;
        private readonly ILogger<PollController> _logger;

        public PollController(IPollIdGenerator pollIdGenerator, IPollRepository pollRepository, ILogger<PollController> logger)
        {
            _pollRepository = pollRepository;
            _pollIdGenerator = pollIdGenerator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll([FromRoute] string id)
        {
            try
            {
                var poll = await _pollRepository.Read(id);
                return new OkObjectResult(poll);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Failed to find poll with ID {id}", e);
                return new NotFoundObjectResult("No poll found with the given ID");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollRequest poll)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid poll object provided");
            }

            var pollId = _pollIdGenerator.Generate();
            try
            {
                await _pollRepository.Create(pollId, poll);
                return new CreatedResult($"{ControllerContext.HttpContext.Request.GetDisplayUrl()}/{pollId}", poll);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError("Failed to create new poll ", e);
                return new BadRequestObjectResult("Invalid poll object");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SetPollState(
            [FromRoute] string id, [FromBody] SetPollStateRequest setPollStateRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult("Invalid poll state provided");
            }

            PollResponse poll;
            try
            {
                poll = await _pollRepository.Read(id);
                if (poll.AdminIdentity != setPollStateRequest.AdminIdentity)
                {
                    return new UnauthorizedResult();
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Failed to find poll with ID {id}", e);
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            try
            {
                if (setPollStateRequest.AdminIdentity != poll.AdminIdentity)
                    return new UnauthorizedResult();

                await _pollRepository.Update(id, setPollStateRequest);
                return new OkResult();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Failed to update state on poll {id}", e);
                return new BadRequestObjectResult("Invalid poll state provided");
            }
        }
    }
}
