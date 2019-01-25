using System.Collections.Generic;
using ProjectVotestorm.Models.Enums;

namespace ProjectVotestorm.Models
{
    public class CreatePollRequest
    {
        public string Prompt { get; set; }
        public List<string> Options { get; set; }
        public PollType PollType { get; set; }
    }
}
