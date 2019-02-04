using System.Threading.Tasks;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Repositories
{
    public interface IPollRepository
    {
        Task Create(string id, CreatePollRequest pollToCreate);
        Task<PollResponse> Read(string id);
    }
}