using FluentValidation;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Validators
{
    public class CreatePluralityVoteRequestValidator : AbstractValidator<CreatePluralityVoteRequest>
    {
        public CreatePluralityVoteRequestValidator()
        {
            RuleFor(r => r.Identity).NotEmpty().WithMessage("Request must include voter's identity");
            RuleFor(r => r.SelectionIndex).InclusiveBetween(0, 5).WithMessage("Index provided not a valid poll option index");
        }
    }
}