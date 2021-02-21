using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IUserManager
    {
        Task<bool> CheckPassword(IUser user, string password);
        Task<bool> RegisterUserAsync(IUser user);
        Task<string> GenerateIdTokenAsync(IUser user, bool rememberMe);
    }
}
