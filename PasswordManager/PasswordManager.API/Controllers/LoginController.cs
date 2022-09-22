using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Interfaces;
using PasswordManager.Entities.InputModels;
using System.Linq;

namespace PasswordManager.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginController(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = _userService.GetAll().FirstOrDefault(x => x.Username == model.Username);

            if (user is null)
            {
                return StatusCode(401, "Username or password is not correct");
            }

            // Check that the password is correct. If not we un-authorize the request.
            if (!_loginService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return StatusCode(401, "Username or password is not correct");
            }

            // The user exist and the password is correct
            // so we return the username and a token back.
            return Ok(new
            {
                account = new
                {
                    Username = model.Username,
                },
                token = _loginService.GenerateToken(user)
            });
        }

    }
}
