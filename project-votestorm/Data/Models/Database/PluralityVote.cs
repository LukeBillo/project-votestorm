using ProjectVotestorm.Data.Models.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectVotestorm.Data.Models.Database
{
    [Table("PluralityVote")]
    public class PluralityVote : UserVote
    {
        public PluralityVote(CreatePluralityVoteRequest createPluralityVoteRequest, string pollId)
        {
            Identity = createPluralityVoteRequest.Identity;
            PollId = pollId;
            SelectionIndex = createPluralityVoteRequest.SelectionIndex;
        }
        public int SelectionIndex { get; set; }
    }
}
