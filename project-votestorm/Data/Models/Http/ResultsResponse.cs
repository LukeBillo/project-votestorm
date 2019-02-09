using System.Collections.Generic;
using System.Linq;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Enums;

namespace ProjectVotestorm.Data.Models.Http
{
    public class ResultsResponse
    {
        public ResultsResponse(IEnumerable<PluralityVote> votes, PollResponse pollOptions)
        {
            PollType = pollOptions.PollType;
            OptionResults = new List<PluralityOptionResult>();

            var pluralityOptionResults = votes
                .GroupBy(vote => vote.SelectionIndex)
                .Select(voteGroup => new PluralityOptionResult(voteGroup.Count(), pollOptions.Options[voteGroup.Key]));

            OptionResults.AddRange(pluralityOptionResults);
            TotalVotes = OptionResults.Sum(optionResult => optionResult.NumberOfVotes);
        }
        public int TotalVotes { get; set; }
        public PollType PollType { get; set; }
        public List<PluralityOptionResult> OptionResults { get; set; }
    }
}
