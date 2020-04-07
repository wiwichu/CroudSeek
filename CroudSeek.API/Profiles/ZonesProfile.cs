using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class ZonesProfile : Profile
    {
        public ZonesProfile()
        {
            CreateMap<Entities.Zone, Models.ZoneDto>();
        }
    }
}
