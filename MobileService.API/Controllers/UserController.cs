using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobileService.API.Services;
using MobileService.Entities.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;

namespace MobileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGenerateToken _generateToken;

        public UserController(UserManager<IdentityUser> userManager,
            IGenerateToken generateToken)
        {
            _userManager = userManager;
            _generateToken = generateToken;
        }

        [HttpGet]
        [Route("getuserid")]
        public async Task<IActionResult> GetUserId()
        { 
            return Ok(new { userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value });
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (userModel == null)
                return BadRequest();

            var user = new IdentityUser()
            {
                UserName = userModel.Username,
                Email = ""
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Username);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userModel.Password);

            if (isPasswordCorrect)
            {
                return Ok(new { token = _generateToken.GenerateTokenMethod(user.Id) });
            }

            return NotFound();
        }
    }
}
