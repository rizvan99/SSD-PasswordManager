using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Utils;
using PasswordManager.Entities.Domain;
using PasswordManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Implementation
{
    public class ManagerService : IManagerService
    {

        private readonly IManagerRepo _managerRepo;
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;

        public ManagerService(IManagerRepo managerRepo, IUserService userService, IEncryptionService encryptionService)
        {
            _managerRepo = managerRepo;
            _userService = userService;
            _encryptionService = encryptionService;
        }

        //private string CreateTempServerSideKey()
        //{
        //    System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create();
        //    aes.GenerateKey();
        //    var key = Convert.ToBase64String(aes.Key);
        //    return key;
        //}

        public string GetEncryptionKeyString(int userId)
        {
            var user = _userService.Get(userId);
            var date = user.DateTimeCreated.ToString();
            var secret = _encryptionService.GetSecret();
            return secret + date;
        }

        // Todo: encrypt before sending to repo
        public Manager Create(Manager entity)
        {
            var key = GetEncryptionKeyString(entity.UserId);
            var newSecurePass = GenerateStrongPassword();
            var managerToCreate = new Manager()
            {
                Password = _encryptionService.Encrypt(newSecurePass, key),
                Service = _encryptionService.Encrypt(entity.Service, key),
                UserId = entity.UserId,
            };
            return _managerRepo.Create(managerToCreate);
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
                MinUppercases = 2,
                MinLowercases = 2,
                MinSpecials = 2,
            };

            return gen.Generate();
        }

        // Todo: decrypt before returning
        public Manager Get(int id)
        {
            throw new NotImplementedException();
        }

        // Todo: decrypt before returning
        public List<Manager> GetAllForUser(int userId)
        {
            var managers = _managerRepo.GetAll(userId);
            var key = GetEncryptionKeyString(userId);
            foreach (var manager in managers)
            {
                manager.Service = _encryptionService.Decrypt(manager.Service, key);
                manager.Password = _encryptionService.Decrypt(manager.Password, key);
            }
            return managers;
        }

        public Manager Update(Manager entity)
        {
            throw new NotImplementedException();
        }
    }
}
