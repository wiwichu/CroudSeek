using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class UserWeightsProfile : Profile
    {
        public UserWeightsProfile()
        {
            CreateMap<Entities.UserWeight, CroudSeek.Shared.UserWeightDto>().ReverseMap();
            CreateMap<Entities.UserWeight, CroudSeek.Shared.UserWeightForCreationDto>().ReverseMap();
            CreateMap<Entities.UserWeight, CroudSeek.Shared.UserWeightForUpdateDto>().ReverseMap();
        }
    }
}
