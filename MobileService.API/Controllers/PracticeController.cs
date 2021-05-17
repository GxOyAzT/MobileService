using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileService.Core.Commands.Practice;
using MobileService.Core.Queries.Practice;
using MobileService.Entities.Enums;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PracticeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PracticeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getrandom")]
        public async Task<IActionResult> GetRandom()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var getRandomFlashcardQuery = new GetRandomFlashcardQ(userId);

            var flashcard = await _mediator.Send(getRandomFlashcardQuery);

            return Ok(flashcard);
        }

        [HttpGet]
        [Route("getrandomexpired")]
        public async Task<IActionResult> GetRandomExpired()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var getRandomExpiredFlashcardQuery = new GetRandomExpiredFlashcardQ(userId);

            var flashcard = await _mediator.Send(getRandomExpiredFlashcardQuery);

            if (flashcard == null)
            {
                return NotFound();
            }

            return Ok(flashcard);
        }

        [HttpGet]
        [Route("getrandomexpiredforchoose")]
        public async Task<IActionResult> GetRandomExpiredForChoose()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var hasUserFlashcardToLearn = new GetRandomExpiredFlashcardQ(userId);

            if (await _mediator.Send(hasUserFlashcardToLearn) == null)
            {
                return NotFound();
            }

            var getRandomExpiredFlashcardQuery = new GetRandomExpiredForChooseQ(userId);

            var flashcard = await _mediator.Send(getRandomExpiredFlashcardQuery);

            if (flashcard == null)
            {
                return BadRequest("You have not enough flashcards to practice this mode.");
            }

            return Ok(flashcard);
        }

        [HttpGet]
        [Route("getrandomexpiredformixed")]
        public async Task<IActionResult> GetRandomExpiredForMixed()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var hasUserFlashcardToLearn = new GetRandomExpiredFlashcardQ(userId);

            if (await _mediator.Send(hasUserFlashcardToLearn) == null)
            {
                return NotFound();
            }

            var getRandomExpiredFlashcardQuery = new GetRandomExpiredForMixedQ(userId);

            var flashcard = await _mediator.Send(getRandomExpiredFlashcardQuery);

            if (flashcard == null)
            {
                return BadRequest("You have not enough flashcards to practice this mode.");
            }

            return Ok(flashcard);
        }

        [HttpGet]
        [Route("updateflashcardprogress/{flashcardProgressId}/{flashcardProgress}")]
        public async Task<IActionResult> UpdateFlashcardProgress(Guid flashcardProgressId, FlashcardProgress flashcardProgress)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var updateFlashcardProgressC = new UpdateFlashcardProgressC(flashcardProgressId, flashcardProgress, userId);

            var actionResult = await _mediator.Send(updateFlashcardProgressC);

            if (actionResult.IsSucceed)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
