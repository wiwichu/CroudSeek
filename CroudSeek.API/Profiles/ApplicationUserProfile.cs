using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap< CroudSeek.Shared.ApplicationUserProfile, Entities.ApplicationUserProfile>().ReverseMap();

            CreateMap< CroudSeek.Shared.ApplicationUserProfileForCreation, Entities.ApplicationUserProfile>()
               .ForMember(m => m.Id, options => options.Ignore())
               .ForMember(m => m.Subject, options => options.Ignore())
               .ForMember(m => m.SubscriptionLevel, options => options.Ignore());
        }
    }
}
