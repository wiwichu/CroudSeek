using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<CroudSeek.Shared.UserDto, CroudSeek.Shared.UserForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.UserDto, CroudSeek.Shared.UserForUpdateDto>().ReverseMap();
        }
    }
}
