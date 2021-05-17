using MediatR;
using MobileService.Core.Queries.Practice;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Practice
{
    public class GetRandomExpiredForChooseH : IRequestHandler<GetRandomExpiredForChooseQ, FlashcardPracticeChooseGetModel>
    {
        private readonly IMediator _mediator;
        private readonly IFlashcardRepo _flashcardRepo;

        public GetRandomExpiredForChooseH(
            IMediator mediator,
            IFlashcardRepo flashcardRepo)
        {
            _mediator = mediator;
            _flashcardRepo = flashcardRepo;
        }

        public async Task<FlashcardPracticeChooseGetModel> Handle(GetRandomExpiredForChooseQ request, CancellationToken cancellationToken)
        {
            var getRandomExpiredFlashcardQ = new GetRandomExpiredFlashcardQ(request.UserId);

            var choosenFlashcard = await _mediator.Send(getRandomExpiredFlashcardQ);

            if (choosenFlashcard == null)
            {
                return null;
            }

            var allPossibleAnswers = (await _flashcardRepo.GetWhereUserId(request.UserId))
                .Select(e => choosenFlashcard.PracticeDirection == PracticeDirection.ForeignToNative ? e.Native : e.Foreign)
                .ToList();

            if (allPossibleAnswers.Count < 3)
            {
                return null;
            }

            var firstIncorrectAns = choosenFlashcard.Back;

            while(firstIncorrectAns == choosenFlashcard.Back)
            {
                firstIncorrectAns = allPossibleAnswers[(new Random()).Next(0, allPossibleAnswers.Count)];
            }

            var secondIncorrectAns = choosenFlashcard.Back;

            while (secondIncorrectAns == choosenFlashcard.Back || secondIncorrectAns == firstIncorrectAns)
            {
                secondIncorrectAns = allPossibleAnswers[(new Random()).Next(0, allPossibleAnswers.Count)];
            }

            switch((new Random()).Next(1, 4))
            {
                case 1:
                    return new FlashcardPracticeChooseGetModel()
                    {
                        FlashcardProgressId = choosenFlashcard.FlashcardProgressId,
                        CorrectAns = 1,
                        Front = choosenFlashcard.Front,
                        AnsA = choosenFlashcard.Back,
                        AnsB = firstIncorrectAns,
                        AnsC = secondIncorrectAns
                    };
                case 2:
                    return new FlashcardPracticeChooseGetModel()
                    {
                        FlashcardProgressId = choosenFlashcard.FlashcardProgressId,
                        CorrectAns = 2,
                        Front = choosenFlashcard.Front,
                        AnsA = firstIncorrectAns,
                        AnsB = choosenFlashcard.Back,
                        AnsC = secondIncorrectAns
                    };
                case 3:
                    return new FlashcardPracticeChooseGetModel()
                    {
                        FlashcardProgressId = choosenFlashcard.FlashcardProgressId,
                        CorrectAns = 3,
                        Front = choosenFlashcard.Front,
                        AnsA = firstIncorrectAns,
                        AnsB = secondIncorrectAns,
                        AnsC = choosenFlashcard.Back,
                    };
                default:
                    return null;
            }
        }
    }
}
