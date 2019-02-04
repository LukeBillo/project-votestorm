using Dapper.Contrib.Extensions;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Models.Database
{
    [Table("PollOptions")]
    public class PollOption
    {
        /*
         * Default constructor used by dapper;
         * do not remove.
         */
        public PollOption() {}

        public PollOption(string id, string text)
        {
            PollId = id;
            OptionText = text;
        }

        [ExplicitKey]
        public string PollId { get; set; }
        [ExplicitKey]
        public string OptionText { get; set; }
    }
}
