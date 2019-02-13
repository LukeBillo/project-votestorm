using System.Text.RegularExpressions;
using NUnit.Framework;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.UnitTests.PollIdGeneratorTests
{
    [TestFixture]
    public class GivenAPollIdGenerationRequest
    {
        private string _generatedPollId;

        [OneTimeSetUp]
        public void WhenCallingGenerateOnThePollIdGenerator()
        {
            var pollIdGenerator = new PollIdGenerator();
            _generatedPollId = pollIdGenerator.Generate();
        }

        [Test]
        public void ThenThePollIdMatchesRegex()
        {
            Assert.That(Regex.IsMatch(_generatedPollId, "^[A-Z1-9]{5}$"));
        }
    }
}
