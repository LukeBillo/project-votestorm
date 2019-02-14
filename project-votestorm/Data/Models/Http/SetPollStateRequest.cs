using System.Collections.Generic;

namespace ProjectVotestorm.Data.Models.Http
{
    public class SetPollStateRequest
    {
        public bool IsActive { get; set; }
        public string AdminIdentity { get; set; }
    }
}
