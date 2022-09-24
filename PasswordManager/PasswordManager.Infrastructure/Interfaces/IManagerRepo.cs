using PasswordManager.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Interfaces
{
    public interface IManagerRepo
    {
        public Manager Create(Manager entity);
        public Manager Update(Manager entity);
        public Manager Delete(int id);
        public Manager Get(int id);
        public List<Manager> GetAll(int userId);
    }
}
