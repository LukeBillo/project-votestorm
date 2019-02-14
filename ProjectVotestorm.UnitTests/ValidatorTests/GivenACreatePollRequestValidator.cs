using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ProjectVotestorm.Data.Validators;

namespace ProjectVotestorm.UnitTests.ValidatorTests
{
    [TestFixture]
    public class GivenACreatePollRequestValidator
    {
        private CreatePollRequestValidator _validator;

        [OneTimeSetUp]
        public void WhenACreatePollRequestValidatorIsCreated()
        {
            _validator = new CreatePollRequestValidator();
        }

        [Test]
        public void ThenAnyStringPromptIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.Prompt, "I am a prompt");
        }

        [Test]
        public void ThenAnyOptionsListWithLengthBetweenTwoAndFiveIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.Options, new List<string> { "1", "2" });
            _validator.ShouldNotHaveValidationErrorFor(request => request.Options, new List<string> { "1", "2", "3","4", "5" });
        }

        [Test]
        public void ThenAnyStringAdminIdentityIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.AdminIdentity, Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenAnEmptyStringPromptIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.Prompt, "");
        }

        [Test]
        public void ThenAnIncorrectSizeListIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.Options, new List<string>());
            _validator.ShouldHaveValidationErrorFor(request => request.Options, new List<string> { "1" });
            _validator.ShouldHaveValidationErrorFor(request => request.Options, new List<string> { "1", "2", "3", "4", "5", "6" });
        }

        [Test]
        public void ThenAnEmptyOptionIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.Options, new List<string> { "", "" });
            _validator.ShouldHaveValidationErrorFor(request => request.Options, new List<string> { "1", "", "" });
        }

        [Test]
        public void ThenAnEmptyStringAdminIdentityIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.AdminIdentity, "");
            _validator.ShouldHaveValidationErrorFor(request => request.AdminIdentity, null as string);
        }
    }
}
