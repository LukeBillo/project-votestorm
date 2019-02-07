using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{id}/results")]
    public class ResultsController : Controller
    {
        private readonly IPollRepository _pollRepository;
        private readonly IPollIdGenerator _pollIdGenerator;

        public ResultsController(IPollIdGenerator pollIdGenerator, IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
            _pollIdGenerator = pollIdGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoll([FromRoute] string id)
        {
            var poll = await _pollRepository.Read(id);
            return new OkObjectResult(poll);
        }
    }
}
