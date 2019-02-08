using Dapper;
using Dapper.Contrib.Extensions;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectVotestorm.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly SqlConnectionManager _connectionManager;

        public VoteRepository(SqlConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;

            using (var connection = _connectionManager.GetConnection())
            {
                connection.Execute(@"CREATE TABLE IF NOT EXISTS PluralityVote
                (pollId VARCHAR(5),
                identity VARCHAR(50),
                selectionIndex INTEGER,
                PRIMARY KEY (pollId, identity))");
            }
        }

        public async Task<IEnumerable<PluralityVote>> Get(string pollId)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                return await connection.QueryAsync<PluralityVote>(
                    "SELECT * FROM PluralityVote WHERE pollId = @pollId",
                    new {pollId});
            }
        }

        public async Task Create(CreatePluralityVoteRequest pollToCreate, string pollId)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                var voteToInsert = new PluralityVote(pollToCreate, pollId);
                await connection.InsertAsync(voteToInsert);
            }
        }
    }
}
