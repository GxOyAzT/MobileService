using MediatR;
using MobileService.Core.Queries.Collections;
using MobileService.Core.Queries.Flashcards;
using MobileService.Core.WorkUnits;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace MobileService.Core.Handlers.Flashcards
{
    public class GetFlashcardsListWithProgressesH : IRequestHandler<GetFlashcardsListWithProgressQ, List<FlashcardWithProgressionGetModel>>
    {
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IMediator _mediator;
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetFlashcardsListWithProgressesH(
            IFlashcardRepo flashcardRepo,
            IMediator mediator,
            IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardRepo = flashcardRepo;
            _mediator = mediator;
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<List<FlashcardWithProgressionGetModel>> Handle(GetFlashcardsListWithProgressQ request, CancellationToken cancellationToken)
        {
            if (await _mediator.Send(new GetCollectionByIdWithDailyStatsQ(request.CollectionId, request.UserId)) == null)
            {
                return new List<FlashcardWithProgressionGetModel>();
            }

            var flashcards = await _flashcardRepo.GetWhereCollectionId(request.CollectionId);

            var output = new List<FlashcardWithProgressionGetModel>();

            var progressModels = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            foreach (var flashcard in flashcards)
            {
                var nativeToForeign = progressModels.FirstOrDefault(e => e.FlashcardModelId == flashcard.Id && e.PracticeDirection == PracticeDirection.NativeToForeign);
                var foreignToNative = progressModels.FirstOrDefault(e => e.FlashcardModelId == flashcard.Id && e.PracticeDirection == PracticeDirection.ForeignToNative);

                output.Add(new FlashcardWithProgressionGetModel()
                {
                    Id = flashcard.Id,
                    Foreign = flashcard.Foreign,
                    Native = flashcard.Native,
                    ProgressForeignToNative = new FlashcardWithProgressGetModel()
                    {
                        CorrectInRow = foreignToNative.CorrectInRow,
                        PracticeDate = foreignToNative.PracticeDate.ToString("dd-MM-yyyy"),
                        PracticeDateIfCorrectAns = ((await _mediator.Send(new CalculatePracticeDateQ(foreignToNative.CorrectInRow, FlashcardProgress.Know)))
                                                    .AddDays((foreignToNative.PracticeDate - DateTime.Now.Date).TotalDays)).ToString("dd-MM-yyyy")
                    },
                    ProgressNativeToForeign = new FlashcardWithProgressGetModel()
                    {
                        CorrectInRow = nativeToForeign.CorrectInRow,
                        PracticeDate = nativeToForeign.PracticeDate.ToString("dd-MM-yyyy"),
                        PracticeDateIfCorrectAns = ((await _mediator.Send(new CalculatePracticeDateQ(nativeToForeign.CorrectInRow, FlashcardProgress.Know)))
                                                    .AddDays((nativeToForeign.PracticeDate - DateTime.Now.Date).TotalDays)).ToString("dd-MM-yyyy")
                    }
                });
            }

            return output;
        }
    }
}
