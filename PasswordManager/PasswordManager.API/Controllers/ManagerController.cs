using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Utils;
using PasswordManager.Entities.Domain;
using PasswordManager.Entities.DTO;
using System;
using System.Linq;
using System.Security.Claims;

namespace PasswordManager.API.Controllers
{
    [Route("manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult PostManager([FromBody] ManagerDTO dto)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid)).Value;

                if (userId == null)
                {
                    return Unauthorized();
                }
                var gen = new PasswordGenerator()
                {
                    Length = 12,
                    MinDigits = 3,
                    MinUppercases = 1,
                    MinLowercases = 1,
                    MinSpecials = 1,
                };

                var newManager = new Manager()
                {
                    UserId = int.Parse(userId),
                    Service = dto.Service,
                    Password = gen.Generate(), // handle at service?
                };

                var res = _managerService.Create(newManager);

                return Ok(res);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Todo: make GET methods to retrieve managers -> get encrypted data from database -> make service decrypt

    }
}
