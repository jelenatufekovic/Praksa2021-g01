using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class RefereeService : IRefereeService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public RefereeService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<IReferee> CreateRefereeAsync(IReferee referee)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                referee.Id = await unitOfWork.Person.CreatePersonAsync(referee);
                await unitOfWork.Referee.CreateRefereeAsync(referee);

                IReferee createdReferee = await unitOfWork.Referee.GetRefereeByIdAsync(referee.Id);

                unitOfWork.Commit();

                return createdReferee;
            }
        }

        public async Task<bool> DeleteRefereeAsync(Guid id, Guid userId)
        {
            IRefereeRepository refereeRepository = _repositoryFactory.GetRepository<RefereeRepository>();

            return await refereeRepository.DeleteRefereeAsync(id, userId);
        }

        public async Task<IReferee> GetRefereeByIdAsync(Guid id)
        {
            IRefereeRepository refereeRepository = _repositoryFactory.GetRepository<RefereeRepository>();

            return await refereeRepository.GetRefereeByIdAsync(id);
        }

        public async Task<PagedList<IReferee>> FindRefereesAsync(RefereeParameters parameters)
        {
            IRefereeRepository refereeRepository = _repositoryFactory.GetRepository<RefereeRepository>();

            return await refereeRepository.FindRefereesAsync(parameters);
        }

        public async Task<bool> UpdateRefereeAsync(IReferee referee)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                await unitOfWork.Person.UpdatePersonAsync(referee);
                await unitOfWork.Referee.UpdateRefereeAsync(referee);

                unitOfWork.Commit();

                return true;
            }
        }
    }
}
