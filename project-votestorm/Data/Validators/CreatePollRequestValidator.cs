using FluentValidation;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Validators
{
    public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(r => r.Prompt).NotEmpty().WithMessage("Poll prompt must not be empty");

            RuleFor(r => r.Options).NotEmpty().Must(r => r != null && r.Count >= 2 && r.Count <= 5).WithMessage("Poll must have at least 2 and at most 5 options");
            RuleForEach(r => r.Options).NotEmpty().WithMessage("Poll options must not be empty");

            RuleFor(r => r.AdminIdentity).NotEmpty().WithMessage("Poll request must include admin identity");
        }
    }
}