using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class StatisticsProfile: Profile
    {
        #region Constructors

        public StatisticsProfile()
        {
            CreateMap<CreateStatisticsRest, IStatistics>();
            CreateMap<IStatistics, StatisticsViewModel>();
            CreateMap<UpdateStatisticsRest, IStatistics>();
        }

        #endregion Constructors
    }
}