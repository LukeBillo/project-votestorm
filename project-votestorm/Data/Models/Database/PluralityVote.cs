using ProjectVotestorm.Data.Models.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectVotestorm.Data.Models.Database
{
    [Table("PluralityVote")]
    public class PluralityVote : UserVote
    {
        /*
         * Default constructors used by Dapper;
         * do not remove.
         */
        public PluralityVote() {}

        public PluralityVote(CreatePluralityVoteRequest createPluralityVoteRequest, string pollId)
        {
            Identity = createPluralityVoteRequest.Identity;
            PollId = pollId;
            SelectionIndex = createPluralityVoteRequest.SelectionIndex;
        }

        public int SelectionIndex { get; set; }
    }
}
