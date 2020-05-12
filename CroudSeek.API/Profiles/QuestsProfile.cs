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
            CreateMap<Entities.Quest, CroudSeek.Shared.QuestDto>();
            CreateMap<Entities.Quest, CroudSeek.Shared.QuestForCreationDto>().ReverseMap();
            CreateMap<Entities.Quest, CroudSeek.Shared.QuestForUpdateDto>().ReverseMap();
        }
    }
}
