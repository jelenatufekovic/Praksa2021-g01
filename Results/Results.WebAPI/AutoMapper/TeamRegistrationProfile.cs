using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.TeamSeason;
using Results.WebAPI.Models.ViewModels;

namespace Results.WebAPI.AutoMapper
{
    public class TeamRegistrationProfile : Profile
    {
        public TeamRegistrationProfile()
        {
            CreateMap<TeamRegistrationRest, ITeamRegistration>();
            CreateMap<ITeamRegistration, TeamRegistrationView>();
        }

    }
}