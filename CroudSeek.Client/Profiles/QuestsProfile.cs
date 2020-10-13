using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.Client.Profiles
{
    public class QuestsProfile : Profile
    {
        public QuestsProfile()
        {
            CreateMap<CroudSeek.Shared.QuestDto, CroudSeek.Shared.QuestForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.QuestForUpdateDto, CroudSeek.Shared.QuestForCreationDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.QuestDto, CroudSeek.Shared.QuestForUpdateDto>().ReverseMap();
            CreateMap<CroudSeek.Shared.QuestDto, CroudSeek.Shared.QuestWithDataPointsDto>().ReverseMap();
        }
    }
}
