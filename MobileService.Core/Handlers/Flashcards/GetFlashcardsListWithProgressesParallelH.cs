using MediatR;
using MobileService.Core.Queries.Collections;
using MobileService.Core.Queries.Flashcards;
using MobileService.Core.WorkUnits;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class GetFlashcardsListWithProgressesParallelH : IRequestHandler<GetFlashcardsListWithProgressesParallelQ, List<FlashcardWithProgressionGetModel>>
    {
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IMediator _mediator;
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetFlashcardsListWithProgressesParallelH(
            IFlashcardRepo flashcardRepo,
            IMediator mediator,
            IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardRepo = flashcardRepo;
            _mediator = mediator;
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<List<FlashcardWithProgressionGetModel>> Handle(GetFlashcardsListWithProgressesParallelQ request, CancellationToken cancellationToken)
        {
            if (await _mediator.Send(new GetCollectionByIdWithDailyStatsQ(request.CollectionId, request.UserId)) == null)
            {
                return new List<FlashcardWithProgressionGetModel>();
            }

            var flashcards = await _flashcardRepo.GetWhereCollectionId(request.CollectionId);

            var output = new ConcurrentBag<FlashcardWithProgressionGetModel>();

            var progressModels = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            Parallel.ForEach(flashcards, async flashcard =>
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
            });
            
            return output.ToList();
        }
    }
}
