using MediatR;
using MobileService.Core.Commands.Flashcards;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using MobileService.Entities.Enums;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class InsertFlashcardH : IRequestHandler<InsertFlashcardC, ActionReponseModel>
    {
        private readonly IMediator _mediator;
        private readonly IFlashcardRepo _flashcardRepo;

        public InsertFlashcardH(
            IMediator mediator,
            IFlashcardRepo flashcardRepo)
        {
            _mediator = mediator;
            _flashcardRepo = flashcardRepo;
        }

        public async Task<ActionReponseModel> Handle(InsertFlashcardC request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Flashcard.Foreign) || string.IsNullOrEmpty(request.Flashcard.Native))
            {
                return new ActionReponseModel(false, "Foreign and native cannot be empty value.");
            }

            var getCollectionC = new GetCollectionByIdWithDailyStatsQ(request.Flashcard.CollectionId, request.UserId);

            if (await _mediator.Send(getCollectionC) == null)
            {
                return new ActionReponseModel(false, $"Cannot find collection of id {request.UserId}");
            }

            var inputModel = new FlashcardModel()
            {
                Foreign = request.Flashcard.Foreign,
                Native = request.Flashcard.Native,
                CollectionModelId = request.Flashcard.CollectionId,
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        PracticeDate = DateTime.MinValue,
                        CorrectInRow = 0
                    },
                    new FlashcardProgressModel()
                    {
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        PracticeDate = DateTime.MinValue,
                        CorrectInRow = 0
                    }
                }
            };

            await _flashcardRepo.Insert(inputModel);

            return new ActionReponseModel(true, string.Empty);
        }
    }
}
