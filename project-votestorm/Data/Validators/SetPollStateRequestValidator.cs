using FluentValidation;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Validators
{
    public class SetPollStateRequestValidator : AbstractValidator<SetPollStateRequest>
    {
        public SetPollStateRequestValidator()
        {
            RuleFor(r => r.AdminIdentity).NotEmpty().WithMessage("Request must include admin identity");
        }
    }
}