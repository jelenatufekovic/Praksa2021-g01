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

        public async Task<Guid> CreateRefereeAsync(IReferee referee)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                referee.Id = await unitOfWork.Person.CreatePersonAsync(referee);
                await unitOfWork.Referee.CreateRefereeAsync(referee);

                unitOfWork.Commit();

                return referee.Id;
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

        public async Task<PagedList<IReferee>> GetRefereeByQueryAsync(RefereeParameters parameters)
        {
            IRefereeRepository refereeRepository = _repositoryFactory.GetRepository<RefereeRepository>();

            return await refereeRepository.GetRefereeByQueryAsync(parameters);
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
