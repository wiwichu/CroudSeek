using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Entities.User, CroudSeek.Shared.UserDto>().ReverseMap();
            CreateMap<Entities.User, CroudSeek.Shared.UserForCreationDto>().ReverseMap();
            CreateMap<Entities.User, CroudSeek.Shared.UserForUpdateDto>().ReverseMap();
        }
    }
}
