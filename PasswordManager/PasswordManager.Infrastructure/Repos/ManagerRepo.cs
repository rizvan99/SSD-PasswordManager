using PasswordManager.Entities.Domain;
using PasswordManager.Infrastructure.Context;
using PasswordManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Repos
{
    public class ManagerRepo : IManagerRepo
    {
        public PasswordManagerContext _ctx;

        public ManagerRepo(PasswordManagerContext ctx)
        {
            _ctx = ctx;
        }

        public Manager Create(Manager entity)
        {
            throw new NotImplementedException();
        }

        public Manager Delete(int id)
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
