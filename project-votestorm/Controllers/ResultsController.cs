using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{id}/results")]
    public class ResultsController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public ResultsController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoll([FromRoute] string id)
        {
            var poll = await _pollRepository.Read(id);
            return new OkObjectResult(poll);
        }
    }
}
