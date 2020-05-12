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
            CreateMap<Entities.DataPoint, CroudSeek.Shared.DataPointDto>();
            CreateMap<Entities.DataPoint, CroudSeek.Shared.DataPointForCreationDto>().ReverseMap();
            CreateMap<Entities.DataPoint, CroudSeek.Shared.DataPointForManipulationDto>().ReverseMap();
            CreateMap<Entities.DataPoint, CroudSeek.Shared.DataPointForUpdateDto>().ReverseMap();            
        }
    }
}
