using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class ViewsProfile : Profile
    {
        public ViewsProfile()
        {
            CreateMap<Entities.View, CroudSeek.Shared.ViewDto>().ReverseMap();
            CreateMap<Entities.View, CroudSeek.Shared.ViewForCreationDto>().ReverseMap();
            CreateMap<Entities.View, CroudSeek.Shared.ViewForUpdateDto>().ReverseMap();
            CreateMap<Entities.View, CroudSeek.Shared.ViewWithoutUserWeightsDto>().ReverseMap();
        }
    }
}
