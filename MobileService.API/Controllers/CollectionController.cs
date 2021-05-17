using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileService.Core.Commands.Collections;
using MobileService.Core.Queries;
using MobileService.Core.Queries.Collections;
using MobileService.Entities.DataTransferModels.Collection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CollectionController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var getCollectionsListQ = new GetCollectionsListQ(userId);

            var collectionList = await _mediator.Send(getCollectionsListQ);

            return Ok(_mapper.Map<List<CollectionGetModel>>(collectionList));
        }

        [HttpGet]
        [Route("get/{collectionId}")]
        public async Task<IActionResult> Get(Guid collectionId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var query = new GetCollectionByIdWithDailyStatsQ(collectionId, userId);

            var collection = await _mediator.Send(query);

            if (collection != null)
            {
                return Ok(collection);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody] CollectionInsertModel collection)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var command = new InsertCollectionC(collection, userId);

            var actionResult = await _mediator.Send(command);

            if (actionResult.IsSucceed == true)
            {
                return Ok();
            }

            return BadRequest(actionResult.Message);
        }

        [HttpDelete]
        [Route("delete/{collectionId}")]
        public async Task<IActionResult> Delete(Guid collectionId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var command = new DeleteCollectionC(collectionId, userId);

            var actionResult = await _mediator.Send(command);

            if (actionResult.IsSucceed == true)
            {
                return Ok();
            }

            return BadRequest(actionResult.Message);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CollectionUpdateModel collection)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var updateCollectionC = new UpdateCollectionC(collection, userId);

            var actionResponse = await _mediator.Send(updateCollectionC);

            if (actionResponse.IsSucceed)
            {
                return Ok();
            }

            return BadRequest(actionResponse.Message);
        }
    }
}
