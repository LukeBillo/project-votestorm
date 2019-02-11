using System;
using System.Collections.Generic;
using System.Text;
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
        public void ThenThePollIdIs5Characters()
        {
            Assert.That(_generatedPollId.Length, Is.EqualTo(5));
        }

        [Test]
        public void ThenThePollIdIsLowercaseAToZ()
        {
            Assert.That(Regex.IsMatch(_generatedPollId, "^[a-z]+$"));
        }
    }
}
