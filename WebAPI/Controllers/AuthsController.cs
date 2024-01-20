using Business.Abstracts;
using Business.Dtos.Request.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthsController : Controller
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateRegisterRequest request)
        {
            
                var registerResult = await _authService.Register(request);

                if(registerResult.IsSuccess) 
                {
                    var result = _authService.CreateAccessToken(registerResult.Data);
                    return Ok(result);
                }

                return BadRequest(registerResult.Data);
            

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(CreateLoginRequest request)
        {
            var userToLogin = await _authService.Login(request);
            if (!userToLogin.IsSuccess)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
