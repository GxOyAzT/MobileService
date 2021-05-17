using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobileService.Core.Queries.StatsUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getuserweekstats")]
        public async Task<IActionResult> GetUserWeekStats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var query = new GetStatsUserWeekQ(userId);

            var outputModel = await _mediator.Send(query);

            return Ok(outputModel);
        }

        [HttpGet]
        [Route("getuserdailystats")]
        public async Task<IActionResult> GetUserDailyStats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var query = new GetStatsUserDailyQ(userId);

            var outputModel = await _mediator.Send(query);

            return Ok(outputModel);
        }

        [HttpGet]
        [Route("getusernextweekexpiresstats")]
        public async Task<IActionResult> GetUserNextWeekExpiredStats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var query = new GetStatsUserExpiredNextWeekQ(userId);

            var outputModel = await _mediator.Send(query);

            return Ok(outputModel);
        }

        [HttpGet]
        [Route("getuserprogress")]
        public async Task<IActionResult> GetUserProgress()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var query = new GetStatsUserProgressQ(userId);

            var outputModel = await _mediator.Send(query);

            return Ok(outputModel);
        }
    }
}
