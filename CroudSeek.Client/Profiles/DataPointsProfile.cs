using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Profiles
{
    public class DataPointsProfile : Profile
    {
        public DataPointsProfile()
        {
            CreateMap<CroudSeek.Shared.DataPointDto, CroudSeek.Shared.DataPointForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.DataPointDto, CroudSeek.Shared.DataPointForManipulationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.DataPointDto, CroudSeek.Shared.DataPointForUpdateDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.DataPointForCreationDto, CroudSeek.Shared.DataPointForUpdateDto>().ReverseMap();
        }
    }
}
