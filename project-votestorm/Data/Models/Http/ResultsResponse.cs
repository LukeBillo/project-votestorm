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
            PollType = (int)pollOptions.PollType;
            var votesBySelectionIndex = votes.GroupBy(vote => vote.SelectionIndex);
            OptionResults = new List<PluralityOptionResult>();
            foreach (var vote in votesBySelectionIndex)
            {
                int voteCount = vote.Count();
                TotalVotes += voteCount;
                PluralityOptionResult optionResult =
                new PluralityOptionResult(voteCount, pollOptions.Options[vote.Key]);

                OptionResults.Add(optionResult);
            }
        }
        public int TotalVotes { get; set; }
        public int PollType { get; set; }
        public List<PluralityOptionResult> OptionResults { get; set; }
    }
}
