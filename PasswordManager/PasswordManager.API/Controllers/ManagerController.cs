using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Interfaces;
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

                var newManager = new Manager()
                {
                    UserId = int.Parse(userId),
                    Service = dto.Service,
                    Password = dto.Password
                };

                // Todo: send to service to encrypt -> send to repo to save to database

                return Ok("you have access");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Todo: make GET methods to retrieve managers -> get encrypted data from database -> make service decrypt

    }
}
