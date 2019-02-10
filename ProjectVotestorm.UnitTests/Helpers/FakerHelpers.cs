using System.Linq;
using Bogus;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Models.Http;

namespace ProjectVotestorm.UnitTests.Helpers
{
    public static class FakerHelpers
    {
        public static readonly Faker<PollResponse> PollFaker = new Faker<PollResponse>()
            .CustomInstantiator(faker =>
            {
                var poll = new Poll
                {
                    Id = faker.Random.String2(5),
                    Prompt = faker.Random.Words() + "?",
                    PollType = PollType.Plurality,
                    AdminIdentity = faker.Random.Guid().ToString(),
                    IsActive = true
                };

                var pollOptions = faker.Random.WordsArray(2, 5).Select((word, i) => new PollOption(poll.Id, word, i));

                return new PollResponse(poll, pollOptions);
            });

        public static readonly Faker<CreatePollRequest> CreatePollRequestFaker = new Faker<CreatePollRequest>()
            .RuleFor(request => request.Prompt, f => f.Random.Words() + "?")
            .RuleFor(request => request.PollType, _ => PollType.Plurality)
            .RuleFor(request => request.Options, f => f.Random.WordsArray(2, 5).ToList())
            .RuleFor(request => request.AdminIdentity, f => f.Random.Guid().ToString());
    }
}
