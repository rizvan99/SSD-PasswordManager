using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Interfaces;
using PasswordManager.Entities.Domain;
using PasswordManager.Entities.InputModels;
using System;

namespace PasswordManager.API.Controllers
{
    [Route("register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public RegisterController(IUserService userService, ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterInputModel model)
        {
            try
            {
                _loginService.CreatePasswordHash(model.Password, out var passHash, out var passSalt);

                var user = _userService.Create(new User
                {
                    Username = model.Username,
                    PasswordHash = passHash,
                    PasswordSalt = passSalt,
                    DateTimeCreated = DateTime.Now
                });

                return Ok(new
                {
                    account = new
                    {
                        user.Username,
                    },
                    token = _loginService.GenerateToken(user)
                });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
