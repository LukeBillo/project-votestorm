using Dapper.Contrib.Extensions;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Models.Database
{
    [Table("Poll")]
    public class Poll
    {
        /*
         * Default constructors used by Dapper;
         * do not remove.
         */
        public Poll() {}

        public Poll(string id, CreatePollRequest createPollRequest)
        {
            Id = id;
            Prompt = createPollRequest.Prompt;
            PollType = createPollRequest.PollType;
        }

        [ExplicitKey]
        public string Id { get; set; }
        public string Prompt { get; set; }
        public PollType PollType { get; set; }
    }
}
