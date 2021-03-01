using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Position;

namespace Results.WebAPI.AutoMapper
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<PositionRest, IPosition>();
            CreateMap<IPosition,PositionRest>();
        }
    }
}