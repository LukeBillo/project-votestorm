﻿using System;
using System.Linq;
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
                isOpen BOOLEAN, adminID VARCHAR(40))");

                connection.Execute(@"CREATE TABLE IF NOT EXISTS PollOptions
                (pollId VARCHAR(5), optionText VARCHAR(256), optionIndex INTEGER)");
            }
        }

        public async Task Create(string id, CreatePollRequest pollToCreate)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                var pollToInsert = new Poll(id, pollToCreate);
                await connection.InsertAsync(pollToInsert);

                for (int optionIndex = 0; optionIndex < pollToCreate.Options.Count; optionIndex++)
                {
                    string option = pollToCreate.Options[optionIndex];

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
    }
}