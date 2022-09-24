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

                var newManager = new Manager()
                {
                    UserId = int.Parse(userId),
                    Service = dto.Service,
                };

                var res = _managerService.Create(newManager);

                return Ok($"Created password manager for {dto.Service}!");
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetManagersForLoggedInUser()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid)).Value;

                if (userId == null)
                {
                    return Unauthorized();
                }

                int id = int.Parse(userId);

                var res = _managerService.GetAllForUser(id)
                    .Select(x => new ManagerWithPassDTO()
                    {
                        UserId = x.UserId,
                        Service = x.Service,
                        Password = x.Password,
                    });

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
