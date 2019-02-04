using System;
using System.Linq;

namespace ProjectVotestorm.Data.Utils
{
    public class PollIdGenerator : IPollIdGenerator
    {
        private const string CharactersForGeneration = "abcdefghijklmnopqrstuvwxyz";
        private const int PollIdLength = 5;

        public string Generate()
        {
            var random = new Random();

            var characters = Enumerable.Range(0, PollIdLength)
                .Select(i => CharactersForGeneration[random.Next(0, CharactersForGeneration.Length)]);

            return new string(characters.ToArray());
        }
    }
}
