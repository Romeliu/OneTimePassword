using API.Domain;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly ITemporaryPasswordService _temporaryPasswordService;
        private readonly ITokenService _tokenService;
        public PasswordController(ITemporaryPasswordService temporaryPasswordService,
            ITokenService tokenService)
        {
            _tokenService = tokenService;
            _temporaryPasswordService = temporaryPasswordService;
        }

        [HttpPost("temporaryPassword")]
        public async Task<ActionResult> CreateTemporaryPassword([FromBody] DateTime createdAt, string userId)
        {
            var a = DateTime.Now;
            if(createdAt.AddSeconds(30).CompareTo(DateTime.Now) < 0)
                return BadRequest("The token cannot be created for a past date");

            if(userId == null)
                return BadRequest("User Id cannot be null");

            string token = _tokenService.CreateToken(userId, createdAt);

            await _temporaryPasswordService.CreateAsync(userId, token, createdAt);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string password)
        {
            if(password == null)
                return BadRequest("Password cannot be null");

            var result = await _temporaryPasswordService.DeleteAsync(password);

            if (result != null)
                return Ok(password);

            return BadRequest("Invalid token");
        }
    }
}