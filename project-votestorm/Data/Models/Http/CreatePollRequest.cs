using System.Collections.Generic;
using ProjectVotestorm.Data.Models.Enums;

namespace ProjectVotestorm.Data.Models.Http
{
    public class CreatePollRequest
    {
        public string Prompt { get; set; }
        public List<string> Options { get; set; }
        public PollType PollType { get; set; }
        public string AdminIdentity {get; set;}
    }
}
