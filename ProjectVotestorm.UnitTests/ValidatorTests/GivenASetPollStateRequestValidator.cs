using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ProjectVotestorm.Data.Validators;

namespace ProjectVotestorm.UnitTests.ValidatorTests
{
    [TestFixture]
    public class GivenASetPollStateRequestValidator
    {
        private SetPollStateRequestValidator _validator;

        [OneTimeSetUp]
        public void WhenTheSetPollStateRequestValidatorIsCreated()
        {
            _validator = new SetPollStateRequestValidator();
        }

        [Test]
        public void ThenAnyStringAdminIdentityIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.AdminIdentity, Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenAnEmptyStringAdminIdentityIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.AdminIdentity, "");
            _validator.ShouldHaveValidationErrorFor(request => request.AdminIdentity, null as string);
        }

        [Test]
        public void ThenTheIsActiveFieldIsNotChecked()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.IsActive, true);
            _validator.ShouldNotHaveValidationErrorFor(request => request.IsActive, false);
            _validator.ShouldNotHaveValidationErrorFor(request => request.IsActive, default(bool));
        }
    }
}
