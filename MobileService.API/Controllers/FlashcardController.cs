using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileService.Core.Commands.Flashcards;
using MobileService.Core.Queries.Flashcards;
using MobileService.Entities.DataTransferModels.Flashcard;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashcardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FlashcardController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertFlashcard([FromBody] FlashcardInsertModel flashcard)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var insertFlashcardCommand = new InsertFlashcardC(flashcard, userId);

            var actionReponse = await _mediator.Send(insertFlashcardCommand);

            if (actionReponse.IsSucceed)
            {
                return Ok();
            }

            return BadRequest(actionReponse.Message);
        }

        [HttpGet]
        [Route("getlistbycollectionid/{collectionId}")]
        public async Task<IActionResult> GetFlashcards(Guid collectionId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var getFlashcardsListCommand = new GetFlashcardsListQ(collectionId, userId);

            var actionResponse = await _mediator.Send(getFlashcardsListCommand);

            return Ok(_mapper.Map<List<FlashcardGetModel>>(actionResponse));
        }

        [HttpDelete]
        [Route("delete/{flashcardId}")]
        public async Task<IActionResult> DeleteFlashcard(Guid flashcardId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var deleteFlashcardC = new DeleteFlashcardC(flashcardId, userId);

            var actionResponse = await _mediator.Send(deleteFlashcardC);

            if (actionResponse.IsSucceed)
            {
                return Ok();
            }

            return BadRequest(actionResponse.Message);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateFlashcard([FromBody] FlashcardUpdateModel flashcard)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var updateFlashcardC = new UpdateFlashcardC(flashcard, userId);

            var actionResponse = await _mediator.Send(updateFlashcardC);

            if (actionResponse.IsSucceed)
            {
                return Ok();
            }

            return BadRequest(actionResponse.Message);
        }
    }
}
