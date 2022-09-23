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
    public class UserRepo : IUserRepo
    {
        public PasswordManagerContext _ctx;
        public UserRepo(PasswordManagerContext ctx)
        {
            _ctx = ctx;
        }

        public User Create(User entity)
        {
            var addedUser = _ctx.Users.Add(entity);
            _ctx.SaveChanges();
            return addedUser.Entity;
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return _ctx.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
