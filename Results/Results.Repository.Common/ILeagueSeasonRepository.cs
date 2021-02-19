﻿using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface ILeagueSeasonRepository
    {
        Task<List<ILeagueSeason>> GetAllLeagueSeasonIdAsync();
        Task<Guid> LeagueSeasonRegistrationAsync(ILeagueSeason leagueSeasonModel);
    }
}
