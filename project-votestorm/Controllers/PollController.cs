using System;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Models;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll")]
    public class PollController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult GetPoll()
        {
            throw new NotImplementedException();
        }

        [HttpPost("create")]
        public IActionResult CreatePoll([FromBody] Poll poll)
        {

        }
    }
}
