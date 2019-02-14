using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ProjectVotestorm.Data.Validators;

namespace ProjectVotestorm.UnitTests.ValidatorTests
{
    [TestFixture]
    public class GivenACreatePluralityVoteRequestValidator
    {
        private CreatePluralityVoteRequestValidator _validator;

        [OneTimeSetUp]
        public void WhenTheCreatePluralityVoteRequestValidatorIsCreated()
        {
            _validator = new CreatePluralityVoteRequestValidator();
        }

        [Test]
        public void ThenAnyStringIdentityIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.Identity, Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenAnySelectionIndexBetweenZeroAndFourIsAccepted()
        {
            _validator.ShouldNotHaveValidationErrorFor(request => request.SelectionIndex, 0);
            _validator.ShouldNotHaveValidationErrorFor(request => request.SelectionIndex, 1);
            _validator.ShouldNotHaveValidationErrorFor(request => request.SelectionIndex, 2);
            _validator.ShouldNotHaveValidationErrorFor(request => request.SelectionIndex, 3);
            _validator.ShouldNotHaveValidationErrorFor(request => request.SelectionIndex, 4);
        }

        [Test]
        public void ThenANullIdentityIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.Identity, null as string);
        }

        [Test]
        public void ThenASelectionIndexNotBetweenZeroAndFourIsNotAccepted()
        {
            _validator.ShouldHaveValidationErrorFor(request => request.SelectionIndex, -1);
            _validator.ShouldHaveValidationErrorFor(request => request.SelectionIndex, 5);
        }
    }
}
