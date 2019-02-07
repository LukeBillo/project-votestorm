using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectVotestorm.Data.Models;
using ProjectVotestorm.Data.Models.Database;

namespace ProjectVotestorm.Data.Models.Http
{
    public class CreatePluralityVoteRequest
    {
        public string Identity { get; set; } //user's identity
        public int SelectionIndex { get; set; }
    }
}
