using System.Threading.Tasks;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Repositories
{
    public interface IVoteRepository
    {
        Task Create(CreatePluralityVoteRequest pollToCreate, string pollId);
    }
}