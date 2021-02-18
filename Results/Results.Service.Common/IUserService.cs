using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(IUser user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<IUser> GetUserByIdAsync(Guid id);
        Task<IUser> GetUserByEmailAsync(string email);
        Task<IUser> GetUserByUserNameAsync(string userName);
        Task<bool> RestoreUserAsync(string email);
        Task<bool> UpdateUserAsync(IUser user);
    }
}
