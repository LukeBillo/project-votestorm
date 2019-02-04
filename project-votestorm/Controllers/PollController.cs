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

        [HttpGet]
        public IActionResult GetPoll()
        {
            Console.WriteLine("GET poll " + PollRepository);

            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult CreatePoll([FromBody] Poll poll)
        {
            PollRepository.Create(poll);

            return new OkResult();
        }
    }
}
