using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.TeamSeason;
using Results.WebAPI.Models.ViewModels;

namespace Results.WebAPI.AutoMapper
{
    public class TeamSeasonProfile : Profile
    {
        public TeamSeasonProfile()
        {
            CreateMap<CreateTeamSeason, ITeamSeason>();
            CreateMap<ITeamSeason, TeamSeasonView>();
        }
    }
}