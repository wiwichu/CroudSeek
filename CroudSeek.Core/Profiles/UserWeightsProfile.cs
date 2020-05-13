using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Core.Profiles
{
    public class UserWeightsProfile : Profile
    {
        public UserWeightsProfile()
        {
            CreateMap<CroudSeek.Shared.UserWeightDto, CroudSeek.Shared.UserWeightForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.UserWeightDto, CroudSeek.Shared.UserWeightForUpdateDto>().ReverseMap();
        }
    }
}
