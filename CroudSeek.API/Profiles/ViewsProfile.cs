using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class ViewsProfile : Profile
    {
        public ViewsProfile()
        {
            CreateMap<Entities.View, Models.ViewDto>();
        }
    }
}
