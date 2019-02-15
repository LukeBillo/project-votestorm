using FluentValidation;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.Data.Validators
{
    public class CreatePluralityVoteRequestValidator : AbstractValidator<CreatePluralityVoteRequest>
    {
        public CreatePluralityVoteRequestValidator()
        {
            RuleFor(r => r.Identity).NotEmpty().WithMessage("Request must include voter's identity");
            RuleFor(r => r.SelectionIndex).InclusiveBetween(0, 4).WithMessage("Index provided not a valid poll option index");
        }

        public static bool IsPollVoteValid(PollResponse poll, CreatePluralityVoteRequest vote)
        {
            return vote.SelectionIndex < poll.Options.Count && vote.SelectionIndex >= 0;
        }
    }
}