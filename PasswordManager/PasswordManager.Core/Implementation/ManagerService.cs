using PasswordManager.Core.Interfaces;
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

        public Manager Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string GenerateStrongPassword()
        {
            throw new NotImplementedException();
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
