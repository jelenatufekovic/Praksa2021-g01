using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UserService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Guid> CreateUserAsync(IUser user)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.CreateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.DeleteUserAsync(id);
        }

        public async Task<IUser> GetUserByIdAsync(Guid id)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.GetUserByIdAsync(id);
        }

        public async Task<IUser> GetUserByEmailAsync(string email)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IUser> GetUserByUserNameAsync(string usnerName)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.GetUserByUserNameAsync(usnerName);
        }

        public async Task<bool> RestoreUserAsync(string email)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.RestoreUserAsync(email);
        }

        public async Task<bool> UpdateUserAsync(IUser user)
        {
            IUserRepository userRepository = _repositoryFactory.GetRepository<UserRepository>();

            return await userRepository.UpdateUserAsync(user);
        }
    }
}
