using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Controllers
{
    [Route("api/poll/{id}/results")]
    public class ResultsController : Controller
    {
        private readonly IPollRepository _pollRepository;
        private readonly IVoteRepository _voteRepository;

        public ResultsController(IPollRepository pollRepository, IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository; 
            _pollRepository = pollRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetResults([FromRoute] string id, [FromQuery] string adminIdentity)
        {
            var pollOptions = await _pollRepository.Read(id);
            if (pollOptions.AdminIdentity != adminIdentity)
            {
                return new UnauthorizedResult();
            }

            var votes = await _voteRepository.Get(id);
            
            var resultsResponse = new ResultsResponse(votes,pollOptions);
            
            return new OkObjectResult(resultsResponse);
        }
    }
}
