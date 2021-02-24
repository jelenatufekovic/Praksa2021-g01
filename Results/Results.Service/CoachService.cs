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
    public class CoachService : ICoachService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CoachService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<ICoach> CreateCoachAsync(ICoach coach)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                coach.Id = await unitOfWork.Person.CreatePersonAsync(coach);
                await unitOfWork.Coach.CreateCoachAsync(coach);

                ICoach createdCoach = await unitOfWork.Coach.GetCoachByIdAsync(coach.Id);

                unitOfWork.Commit();

                return createdCoach;
            }
        }

        public async Task<bool> DeleteCoachAsync(Guid id, Guid userId)
        {
            ICoachRepository coachRepository = _repositoryFactory.GetRepository<CoachRepository>();

            return await coachRepository.DeleteCoachAsync(id, userId);
        }

        public async Task<ICoach> GetCoachByIdAsync(Guid id)
        {
            ICoachRepository coachRepository = _repositoryFactory.GetRepository<CoachRepository>();

            return await coachRepository.GetCoachByIdAsync(id);
        }

        public async Task<PagedList<ICoach>> FindCoachesAsync(CoachParameters parameters)
        {
            ICoachRepository coachRepository = _repositoryFactory.GetRepository<CoachRepository>();

            return await coachRepository.FindCoachesAsync(parameters);
        }

        public async Task<bool> UpdateCoachAsync(ICoach coach)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                await unitOfWork.Person.UpdatePersonAsync(coach);
                await unitOfWork.Coach.UpdateCoachAsync(coach);

                unitOfWork.Commit();

                return true;
            }
        }
    }
}
