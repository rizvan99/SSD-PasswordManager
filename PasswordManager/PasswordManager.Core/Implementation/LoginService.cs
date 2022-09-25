using Microsoft.IdentityModel.Tokens;
using PasswordManager.Core.Interfaces;
using PasswordManager.Entities.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Implementation
{
    public class LoginService : ILoginService
    {

        private readonly byte[] _secretBytes;
        public LoginService(byte[] secret)
        {
            _secretBytes = secret;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //PasswordValidator(password);
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(_secretBytes),
                    SecurityAlgorithms.HmacSha256)
                ),
                new JwtPayload(null,    // issuer - not needed (ValidateIssuer = false)
                    null,                     // audience - not needed (ValidateAudience = false)
                    claims.ToArray(),
                    DateTime.Now,                           // notBefore
                    DateTime.Now.AddMinutes(60 * 24 * 7)    // expires
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != storedHash[i]).Any();
        }

        /// <summary>
        /// Used to validate master password upon registering
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PasswordValidator(string password)
        {
            if (password.Length < 8 || password.Where(char.IsUpper).Count() >= 1 || password.Where(char.IsDigit).Count() >= 1)
            {
                throw new ArgumentException("Password must be atleast 8 characters long, contain one uppercase letter and one number");
            }
        }
    }
}
