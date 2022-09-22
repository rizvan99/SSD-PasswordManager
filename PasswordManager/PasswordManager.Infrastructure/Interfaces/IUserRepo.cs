using PasswordManager.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Interfaces
{
    public interface IUserRepo
    {
        public User Create(User entity);
        public User Update(User entity);
        public User Delete(int id);
        public User Get(int id);
        public List<User> GetAll();
    }
}
