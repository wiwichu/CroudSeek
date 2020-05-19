using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Profiles
{
    public class ZonesProfile : Profile
    {
        public ZonesProfile()
        {
            CreateMap<CroudSeek.Shared.ZoneDto, CroudSeek.Shared.ZoneForUpdateDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.ZoneDto, CroudSeek.Shared.ZoneForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.ZoneForUpdateDto, CroudSeek.Shared.ZoneForCreationDto>().ReverseMap();
        }
    }
}
