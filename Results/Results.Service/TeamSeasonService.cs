using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Repository;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils;

namespace Results.Service
{
    public class TeamSeasonService : ITeamSeasonService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public TeamSeasonService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Guid> CreateTeamSeasonAsync(ITeamSeason teamSeason)
        {
            ITeamSeasonRepository teamSeasonRepository = _repositoryFactory.GetRepository<TeamSeasonRepository>();

            return await teamSeasonRepository.CreateTeamSeasonAsync(teamSeason);
        }

        public async Task<bool> RegisterTeamAsync(List<ITeamRegistration> toRegister) {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork()) {
                try {
                    foreach (var player in toRegister)
                    {
                        await unitOfWork.TeamRegistration.CreateTeamRegistrationAsync(player);
                    }
                    unitOfWork.Commit();
                    return true;
                } catch {
                    return false;
                }
            }
        }

        public async Task<List<ITeamSeason>> GetTeamSeasonByQueryAsync(Guid clubID) {
            ITeamSeasonRepository teamSeasonRepository = _repositoryFactory.GetRepository<TeamSeasonRepository>();
            return await teamSeasonRepository.GetTeamSeasonAsync(clubID);
        }

        public async Task<List<ITeamRegistration>> GetTeamByIdAsync(Guid teamSeasonID) {
            ITeamRegistrationRepository teamRegistrationRepository = _repositoryFactory.GetRepository<TeamRegistrationRepository>();
            return await teamRegistrationRepository.GetTeamRegistrationsAsync(teamSeasonID);
        }

        public async Task<bool> UpdateTeamAsync(List<Guid> toDelete, List<ITeamRegistration> toRegister) {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                try
                {
                    foreach (var id in toDelete) { 
                        await unitOfWork.TeamRegistration.DeactivateTeamRegistrationAsync(id);
                    }
                    foreach (var player in toRegister)
                    {
                        await unitOfWork.TeamRegistration.CreateTeamRegistrationAsync(player);
                    }
                    unitOfWork.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteTeamAsync(Guid teamSeasonID) {
            ITeamRegistrationRepository teamRegistrationRepository = _repositoryFactory.GetRepository<TeamRegistrationRepository>();
            List<ITeamRegistration> teamPlayers = await teamRegistrationRepository.GetTeamRegistrationsAsync(teamSeasonID, true);
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                try
                {
                    foreach (var player in teamPlayers)
                    {
                        await unitOfWork.TeamRegistration.DeleteTeamRegistrationAsync(player.Id);
                    }
                    await unitOfWork.TeamSeason.DeleteTeamSeasonAsync(teamSeasonID);
                    unitOfWork.Commit();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
