using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Profiles
{
    public class QuestsProfile : Profile
    {
        public QuestsProfile()
        {
            CreateMap<Entities.Quest, Models.QuestDto>();
            CreateMap<Entities.Quest, Models.QuestForCreationDto>().ReverseMap();
        }
    }
}
