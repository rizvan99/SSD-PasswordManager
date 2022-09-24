using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Entities.DTO
{
    public class ManagerWithPassDTO
    {
        public int UserId { get; set; }
        public string Service { get; set; }
        public string Password { get; set; }
    }
}
