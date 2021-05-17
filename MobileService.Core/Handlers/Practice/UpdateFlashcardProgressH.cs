using MediatR;
using MobileService.Core.Commands.Practice;
using MobileService.Core.Commands.StatsUser;
using MobileService.Core.WorkUnits;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using MobileService.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Practice
{
    public class UpdateFlashcardProgressH : IRequestHandler<UpdateFlashcardProgressC, ActionReponseModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;
        private readonly IMediator _mediator;

        public UpdateFlashcardProgressH(
            IFlashcardProgressRepo flashcardProgressRepo,
            IMediator mediator)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
            _mediator = mediator;
        }

        public async Task<ActionReponseModel> Handle(UpdateFlashcardProgressC request, CancellationToken cancellationToken)
        {
            if (request.FlashcardProgress == FlashcardProgress.UnDefined)
            {
                return new ActionReponseModel(false, "Undefined FlashcardProgress Enum");
            }
            
            var flashcardProgressModel = await _flashcardProgressRepo.Get(request.FlashcardProgressId);

            if (flashcardProgressModel == null)
            {
                return new ActionReponseModel(false, "Cannot find flashcard progress model");
            }

            var calculatePracticeDateQ = new CalculatePracticeDateQ(flashcardProgressModel.CorrectInRow, request.FlashcardProgress);

            flashcardProgressModel.PracticeDate = await _mediator.Send(calculatePracticeDateQ);

            switch (request.FlashcardProgress)
            {
                case FlashcardProgress.DontKnow:
                    flashcardProgressModel.CorrectInRow = flashcardProgressModel.CorrectInRow > 2 ? flashcardProgressModel.CorrectInRow - 2 : 0 ;
                    break;
                case FlashcardProgress.Know:
                    flashcardProgressModel.CorrectInRow++;
                    break;
            }

            await _flashcardProgressRepo.Update(flashcardProgressModel);

            var command = new IncrementStatsUserC(request.UserId);
            await _mediator.Send(command);

            return new ActionReponseModel(true);
        }
    }
}
