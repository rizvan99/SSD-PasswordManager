using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Interfaces
{
    public interface IEncryptionService
    {
        public string Encrypt(string clearText, string key);
        public string Decrypt(string cipherText, string key);
        public string GetSecret();
    }
}
