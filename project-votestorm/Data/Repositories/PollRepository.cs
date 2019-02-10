using System;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly SqlConnectionManager _connectionManager;

        public PollRepository(SqlConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;

            using (var connection = _connectionManager.GetConnection())
            {
                connection.Execute(@"CREATE TABLE IF NOT EXISTS Poll
                (id VARCHAR(5) PRIMARY KEY, prompt VARCHAR(256), pollType INTEGER,
                isActive BOOLEAN, adminIdentity VARCHAR(40), createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP)");

                connection.Execute(@"CREATE TABLE IF NOT EXISTS PollOptions
                (pollId VARCHAR(5), optionText VARCHAR(256), optionIndex INTEGER,
                FOREIGN KEY (pollId) REFERENCES Poll (id) ON DELETE CASCADE)");
            }
        }

        public async Task Create(string id, CreatePollRequest pollToCreate)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                var pollToInsert = new Poll(id, pollToCreate);
                await connection.InsertAsync(pollToInsert);

                for (var optionIndex = 0; optionIndex < pollToCreate.Options.Count; optionIndex++)
                {
                    var option = pollToCreate.Options[optionIndex];

                    var optionToInsert = new PollOption(id, option, optionIndex);
                    await connection.InsertAsync(optionToInsert);
                }
            }
        }

        public async Task<PollResponse> Read(string id)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                var poll = await connection.QueryFirstAsync<Poll>("SELECT * FROM Poll WHERE Id = @Id", new { Id = id });
                var pollOptions = await connection.QueryAsync<PollOption>("SELECT * FROM PollOptions WHERE PollId = @Id", new { Id = id });

                return new PollResponse(poll, pollOptions);
            }
        }
        public async Task Update(string id, CreatePollActivateRequest activateRequest){
            using (var connection = _connectionManager.GetConnection())
            {
                await connection.ExecuteAsync(
                "UPDATE Poll set isActive = @isActive WHERE Id = @Id", 
                new {isActive = activateRequest.IsActive, Id = id});               
            }
        }

        public async Task Delete(DateTime olderThan)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                await connection.ExecuteAsync(
                    "DELETE FROM Poll WHERE createdAt <= @MinDate",
                    new { MinDate = olderThan });
            }
        }
    }
}