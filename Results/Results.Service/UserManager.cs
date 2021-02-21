using Results.Common.Utils;
using Results.Model.Common;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserManager(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<bool> CheckPassword(IUser user, string password)
        {
            byte[] salt = Convert.FromBase64String(user.Salt);
            var tryPasswordHash = await Task.Run(() => PasswordHasher.HashUsingPbkdf2(password, salt, 10000));

            if (tryPasswordHash == user.PasswordHash)
            {
                return true;
            }

            return false;
        }

        public async Task<string> GenerateIdTokenAsync(IUser user, bool rememberMe)
        {
            return "Bearer " + await _tokenService.GenerateTokenAsync(user, rememberMe);
        }

        public async Task<bool> RegisterUserAsync(IUser user)
        {
            byte[] salt = PasswordHasher.GenerateRandomSalt();

            user.Salt = Convert.ToBase64String(salt);
            user.PasswordHash = PasswordHasher.HashUsingPbkdf2(user.Password, salt, 10000);

            return await _userService.CreateUserAsync(user) == Guid.Empty ? false : true;
        }
    }
}
