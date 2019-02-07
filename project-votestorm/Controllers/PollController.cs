using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models;
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

        public PollController(IPollIdGenerator pollIdGenerator, IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
            _pollIdGenerator = pollIdGenerator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll([FromRoute] string id)
        {
            var poll = await _pollRepository.Read(id);
            return new OkObjectResult(poll);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollRequest poll)
        {
            var pollId = _pollIdGenerator.Generate();
            //poll.
            await _pollRepository.Create(pollId, poll);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SetPollState(
            [FromRoute] string id, [FromBody] bool isActive )
        {
            var poll = await _pollRepository.Read(id);
            poll.IsOpen = isActive;
            return new OkObjectResult(poll);
        }
    }
}
