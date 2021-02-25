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

        public StatisticsService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> CreateStatisticsAsync(IStatistics statistics)
        {
            return await UpdateClubStandingsWithNewStatistics(statistics);
        }

        public async Task<IStatistics> GetStatisticsAsync(Guid MatchId)
        {
            IStatisticsRepository _statisticsRepository = _repositoryFactory.GetRepository<StatisticsRepository>();
            return await _statisticsRepository.GetStatisticsAsync(MatchId);
        }

        public async Task<bool> UpdateStatisticsAsync(IStatistics statistics)
        {
            return await UpdateClubStandingsWithNewStatistics(statistics);
        }

        public async Task<bool> DeleteStatisticsAsync(Guid MatchId)
        {
            IStatisticsRepository _statisticsRepository = _repositoryFactory.GetRepository<StatisticsRepository>();
            return await _statisticsRepository.DeleteStatisticsAsync(MatchId);
        }

        public async Task<bool> UpdateClubStandingsWithNewStatistics(IStatistics statistics)
        {
            using(IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                List<Guid> clubIDs = await unitOfWork.Statistics.GetClubIDsAsync(statistics.MatchId);

                foreach (Guid Id in clubIDs)
                {
                    if (clubIDs.IndexOf(Id) == 0)
                    {
                        IStandings standingsUpdate = new Standings();

                        if (statistics.HomeGoals > statistics.AwayGoals)
                        {
                            standingsUpdate.Won += 1;
                        }
                        else if (statistics.HomeGoals == statistics.AwayGoals)
                        {
                            standingsUpdate.Draw += 1;
                        }
                        else
                        {
                            standingsUpdate.Lost += 1;
                        }
                        standingsUpdate.GoalsScored += statistics.HomeGoals;
                        standingsUpdate.GoalsConceded += statistics.AwayGoals;
                        standingsUpdate.ByUser = statistics.ByUser;
                        
                    }
                    else if (clubIDs.IndexOf(Id) == 1)
                    {
                        IStandings standingsUpdate = new Standings();

                        if (statistics.AwayGoals > statistics.HomeGoals)
                        {
                            standingsUpdate.Won += 1;
                        }
                        else if (statistics.AwayGoals == statistics.HomeGoals)
                        {
                            standingsUpdate.Draw += 1;
                        }
                        else
                        {
                            standingsUpdate.Lost += 1;
                        }
                        standingsUpdate.GoalsScored += statistics.AwayGoals;
                        standingsUpdate.GoalsConceded += statistics.HomeGoals;
                        standingsUpdate.ByUser = statistics.ByUser;


                    }
                }

                bool result = await unitOfWork.Statistics.CreateStatisticsAsync(statistics);

                return result;
            }
          
        }
    }
}
