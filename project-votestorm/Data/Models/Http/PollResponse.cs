using System.Collections.Generic;
using System.Linq;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Enums;

namespace ProjectVotestorm.Data.Models.Http
{
    public class PollResponse
    {
        public PollResponse(Poll poll, IEnumerable<PollOption> options)
        {
            Id = poll.Id;
            Prompt = poll.Prompt;
            PollType = poll.PollType;
            Options = options.OrderBy(option=>option.OptionIndex)
                .Select(option => option.OptionText).ToList();
        }

        public string Id { get; set; }
        public string Prompt { get; set; }
        public List<string> Options { get; set; }
        public PollType PollType { get; set; }
    }
}
