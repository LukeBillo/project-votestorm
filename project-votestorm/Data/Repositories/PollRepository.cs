using System.Collections.Generic;
using System.Linq;
using Dapper;
using ProjectVotestorm.Models;
using ProjectVotestorm.Models.Enums;

namespace ProjectVotestorm.Data.Repositories
{
    public class PollRepository
    {
        private SqlConnectionManager connectionManager;

        public PollRepository(SqlConnectionManager manager)
        {
            connectionManager = manager;

            using (var connection = connectionManager.GetConnection())
            {
                connection.Execute(@"CREATE TABLE IF NOT EXISTS Poll
(id VARCHAR(5) PRIMARY KEY, prompt VARCHAR(256), pollType INTEGER)");
                connection.Execute(@"CREATE TABLE IF NOT EXISTS PollOptions
(id VARCHAR(5), text VARCHAR(256))");
            }
        }

        public void Create(Poll pollToCreate)
        {
            using (var connection = connectionManager.GetConnection())
            {
                connection.Execute(@"INSERT INTO Poll VALUES (@Id, @Prompt, @PollType)",
                    new
                    {
                        pollToCreate.Id,
                        pollToCreate.Prompt,
                        pollToCreate.PollType
                    });

                var options = pollToCreate.Options
                    .Select(option => new { pollToCreate.Id, Text = option });
                connection.Execute(@"INSERT INTO PollOptions VALUES (@Id, @Text)", options);
            }
        }

        public Poll Read(string id)
        {
            using (var connection = connectionManager.GetConnection())
            {
                var pollData = connection.QueryFirst("SELECT * FROM Poll WHERE id = @Id", new { Id = id });
                var poll = new Poll
                {
                    Id = pollData.id,
                    Prompt = pollData.prompt,
                    PollType = (PollType) pollData.pollType
                };

                var pollOptionData = connection.Query("SELECT * from PollOptions WHERE id = @Id", new { Id = id });
                poll.Options = new List<string>(pollOptionData.Count());
                foreach (var option in pollOptionData)
                {
                    poll.Options.Add(option.text);
                }

                return poll;
            }
        }
    }
}
