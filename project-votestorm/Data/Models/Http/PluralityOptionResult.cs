using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectVotestorm.Data.Models.Http
{
    public class PluralityOptionResult : OptionResult
    {
        public int NumberOfVotes { get; set; }
        public PluralityOptionResult(int numberOfVotes, string optionText)
        {
            OptionText = optionText;
            NumberOfVotes = numberOfVotes;
        }
    }
}
