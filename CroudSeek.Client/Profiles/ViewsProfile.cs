using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Profiles
{
    public class ViewsProfile : Profile
    {
        public ViewsProfile()
        {
            CreateMap<CroudSeek.Shared.ViewDto, CroudSeek.Shared.ViewForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.ViewDto, CroudSeek.Shared.ViewForUpdateDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.ViewForCreationDto, CroudSeek.Shared.ViewForUpdateDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.ViewDto, CroudSeek.Shared.ViewWithoutUserWeightsDto>().ReverseMap();
        }
    }
}
