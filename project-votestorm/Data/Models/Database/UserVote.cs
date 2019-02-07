using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectVotestorm.Data.Models.Database
{
    
    public abstract class UserVote
    {
        [ExplicitKey]
        public string Identity { get; set; }
        [ExplicitKey]
        public string PollId { get; set; }
    }
}
