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
            CreateMap<Entities.UserWeight, Models.UserWeightDto>();
        }
    }
}
