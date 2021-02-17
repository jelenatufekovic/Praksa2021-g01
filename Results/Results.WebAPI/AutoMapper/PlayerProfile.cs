using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Person;
using Results.WebAPI.Models.ViewModels.Person;

namespace Results.WebAPI.AutoMapper
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerRest, IPlayer>();
            CreateMap<IPlayer, PlayerViewModel>();
        }
    }
}