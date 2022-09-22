using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Utils;
using PasswordManager.Entities.Domain;
using PasswordManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Implementation
{
    public class ManagerService : IManagerService
    {

        private readonly IManagerRepo _managerRepo;

        public ManagerService(IManagerRepo managerRepo)
        {
            _managerRepo = managerRepo;
        }

        public Manager Create(Manager entity)
        {
            throw new NotImplementedException();
        }

        public string DecryptData(int userId)
        {
            throw new NotImplementedException();
        }

        public string EncryptData(int userId)
        {
            throw new NotImplementedException();
        }

        public Manager Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string GenerateStrongPassword()
        {
            var gen = new PasswordGenerator()
            {
                Length = 12,
                MinDigits = 3,
                MinUppercases = 1,
                MinLowercases = 1,
                MinSpecials = 1,
            };

            return gen.Generate();
        }

        public Manager Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Manager> GetAll()
        {
            throw new NotImplementedException();
        }

        public Manager Update(Manager entity)
        {
            throw new NotImplementedException();
        }
    }
}
