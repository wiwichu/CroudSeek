using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class DataPointsProfile : Profile
    {
        public DataPointsProfile()
        {
            CreateMap<Entities.DataPoint, Models.DataPointDto>();
        }
    }
}
