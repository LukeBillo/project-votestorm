using System;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Models;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll")]
    public class PollController : Controller
    {
        private readonly PollRepository PollRepository;

        public PollController(PollRepository pollRepo)
        {
            PollRepository = pollRepo;
        }

        [HttpGet("{id}")]
        public IActionResult GetPoll([FromRoute] string id)
        {
            var poll = PollRepository.Read(id);

            return new OkObjectResult(poll);
        }

        [HttpPost]
        public IActionResult CreatePoll([FromBody] Poll poll)
        {
            PollRepository.Create(poll);

            return new OkResult();
        }
    }
}
