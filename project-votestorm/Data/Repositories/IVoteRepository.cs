using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Repositories
{
    public interface IVoteRepository
    {
        Task<IEnumerable<PluralityVote>> Get(string pollId);
        Task Create(CreatePluralityVoteRequest pollToCreate, string pollId);
    }
}