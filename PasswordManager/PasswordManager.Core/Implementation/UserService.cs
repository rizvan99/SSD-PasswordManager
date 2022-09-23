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
    public class UserService : IUserService, IValidator<User>
    {
        public readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User Create(User entity)
        {
            // Validation => Secure Architecture principles
            // Catch exceptions in controller

            ValidateEntity(entity);

            var newUser = _userRepo.Create(entity);
            return newUser;
        }

        /// <summary>
        /// Validate user entity properties
        /// </summary>
        /// <param name="entity"></param>
        public void ValidateEntity(User entity)
        {
            if (string.IsNullOrEmpty(entity.Username))
            {
                throw new ArgumentNullException("Error occurred, no username provided");
            }

            if (entity.PasswordHash is null || entity.PasswordSalt is null)
            {
                throw new ArgumentNullException("Error occurred, please check your provided password");
            }
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return _userRepo.Get(id);
        }

        public List<User> GetAll()
        {
            return _userRepo.GetAll();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
