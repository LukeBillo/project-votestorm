using System.Collections.Generic;
using ProjectVotestorm.Models.Enums;

namespace ProjectVotestorm.Models
{
    public class Poll
    {
        public string Id { get; set; }
        // aka the question or title of the poll
        public string Prompt { get; set; }
        public List<string> Options { get; set; }
        public PollType PollType { get; set; }
    }
}
