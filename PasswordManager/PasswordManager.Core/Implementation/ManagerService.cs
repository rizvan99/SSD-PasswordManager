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
        private readonly string _secretKey;

        public ManagerService(IManagerRepo managerRepo, IUserService userService)
        {
            _managerRepo = managerRepo;
            _userService = userService;
            _secretKey = CreateTempServerSideKey();
        }

        private string CreateTempServerSideKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            return rsa.ToXmlString(true);
        }

        public string GetEncryptionKeyString(int userId)
        {
            var user = _userService.Get(userId);
            var date = user.DateTimeCreated.ToString();
            var secret = _secretKey;
            return secret + date;
        }

        // Todo: encrypt before sending to repo
        public Manager Create(Manager entity)
        {
            var key = GetEncryptionKeyString(entity.UserId);
            var managerToCreate = new Manager()
            {
                Password = Encrypt(entity.Password, key),
                Service = Encrypt(entity.Service, key),
                UserId = entity.UserId,
            };
            return _managerRepo.Create(managerToCreate);
        }

        public static string Decrypt(string cipherText, string key) // Key = User Password Salt + User DateTimeCreated + Secret Key
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public string Encrypt(string clearText, string key) // Key = User Password Salt + User DateTimeCreated + Secret Key
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
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

        // Todo: decrypt before returning
        public Manager Get(int id)
        {
            throw new NotImplementedException();
        }

        // Todo: decrypt before returning
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
