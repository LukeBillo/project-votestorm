using System.Collections.Generic;

namespace ProjectVotestorm.Data.Models.Http
{
    public class CreatePollActivateRequest
    {
        public bool isActive {get;set;}
        public string adminIdentity{get;set;}
    }
}
