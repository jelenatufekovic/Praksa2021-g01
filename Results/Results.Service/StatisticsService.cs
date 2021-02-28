using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class StatisticsService : IStatisticsService
    {
        IRepositoryFactory _repositoryFactory;
        IStandingsRepository _standingsRepository;

        public StatisticsService(IRepositoryFactory repositoryFactory, IStandingsRepository standingsRepository)
        {
            _repositoryFactory = repositoryFactory;
            _standingsRepository = standingsRepository;
        }

        public async Task<bool> CreateStatisticsAsync(IStatistics statistics)
        {
            await UpdateClubStandingsWithNewStatistics(statistics);
            return await _repositoryFactory.GetRepository<StatisticsRepository>().CreateStatisticsAsync(statistics);
        }

        public async Task<IStatistics> GetStatisticsAsync(Guid MatchId)
        {
            IStatisticsRepository _statisticsRepository = _repositoryFactory.GetRepository<StatisticsRepository>();
            return await _statisticsRepository.GetStatisticsAsync(MatchId);
        }

        public async Task<bool> UpdateStatisticsAsync(IStatistics statistics)
        {
            await UpdateClubStandingsWithNewStatistics(statistics);
            return await _repositoryFactory.GetRepository<StatisticsRepository>().UpdateStatisticsAsync(statistics);
        }

        public async Task<bool> DeleteStatisticsAsync(Guid MatchId)
        {
            IStatisticsRepository _statisticsRepository = _repositoryFactory.GetRepository<StatisticsRepository>();
            return await _statisticsRepository.DeleteStatisticsAsync(MatchId);
        }

        public async Task<bool> UpdateClubStandingsWithNewStatistics(IStatistics statistics)
        {
            bool first_club = false;
            bool second_club = false;

            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                List<Guid> clubIDs = await unitOfWork.Statistics.GetClubIDsAsync(statistics.MatchId);

                foreach (Guid Id in clubIDs)
                {
                    if (clubIDs.IndexOf(Id) == 1)
                    {
                        StandingsParameters standingsParameters = new StandingsParameters()
                        {
                            LeagueSeasonID = clubIDs[0],
                            ClubID = clubIDs[1]
                        };
                        IStandings standingsUpdate = await _standingsRepository.GetStandingsByQueryAsync(standingsParameters);

                        if (statistics.HomeGoals > statistics.AwayGoals)
                        {
                            standingsUpdate.Won += 1;
                            standingsUpdate.Points += 3;
                        }
                        else if (statistics.HomeGoals == statistics.AwayGoals)
                        {
                            standingsUpdate.Draw += 1;
                            standingsUpdate.Points += 1;
                        }
                        else
                        {
                            standingsUpdate.Lost += 1;
                        }
                        standingsUpdate.GoalsScored += statistics.HomeGoals;
                        standingsUpdate.GoalsConceded += statistics.AwayGoals;
                        standingsUpdate.ByUser = statistics.ByUser;
                        standingsUpdate.Played++;

                        first_club = await _standingsRepository.UpdateStandingsForClubAsync(standingsUpdate);
                    }
                    else if (clubIDs.IndexOf(Id) == 2)
                    {

                        StandingsParameters standingsParameters = new StandingsParameters()
                        {
                            LeagueSeasonID = clubIDs[0],
                            ClubID = clubIDs[2]
                        };
                        IStandings standingsUpdate = await _standingsRepository.GetStandingsByQueryAsync(standingsParameters);
                        
                        if (statistics.AwayGoals > statistics.HomeGoals)
                        {
                            standingsUpdate.Won += 1;
                            standingsUpdate.Points += 3;
                        }
                        else if (statistics.AwayGoals == statistics.HomeGoals)
                        {
                            standingsUpdate.Draw += 1;
                            standingsUpdate.Points += 1;
                        }
                        else
                        {
                            standingsUpdate.Lost += 1;
                        }
                        standingsUpdate.GoalsScored += statistics.AwayGoals;
                        standingsUpdate.GoalsConceded += statistics.HomeGoals;
                        standingsUpdate.ByUser = statistics.ByUser;
                        standingsUpdate.Played++;

                        second_club = await _standingsRepository.UpdateStandingsForClubAsync(standingsUpdate);
                    }
                }
            }

            return first_club && second_club;
          
        }
    }
}
