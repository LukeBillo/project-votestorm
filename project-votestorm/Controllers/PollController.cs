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
    [ApiController]
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
            var poll = await _pollRepository.Read(id);

            if (poll == null)
            {
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            return new OkObjectResult(poll);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollRequest poll)
        {
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
            var poll = await _pollRepository.Read(id);
            if (poll == null)
            {
                return new NotFoundObjectResult("No poll found with the given ID");
            }

            if (setPollStateRequest.AdminIdentity != poll.AdminIdentity)
                return new UnauthorizedResult();

            try
            {
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
