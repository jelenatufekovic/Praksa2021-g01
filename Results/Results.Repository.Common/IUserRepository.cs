﻿using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(IUser user);
        Task<bool> UpdateUserAsync(IUser user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<IUser> GetUserByIdAsync(Guid id);
        Task<IUser> GetUserByEmailAsync(string email);
        Task<IUser> GetUserByUserNameAsync(string userName);
    }
}
