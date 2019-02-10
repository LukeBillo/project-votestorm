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

        public PollOption(string id, string text, int index)
        {
            PollId = id;
            OptionText = text;
            OptionIndex = index;
        }
        [ExplicitKey]
        public int OptionIndex { get; set; }

        [ExplicitKey]
        public string PollId { get; set; }

        public string OptionText { get; set; }
    }
}
