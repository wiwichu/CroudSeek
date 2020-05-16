using System;
using System.Collections.Generic;

namespace CroudSeek.Shared
{
    public class QuestWithDataPointsDto : QuestDto
    {
        public List<DataPointDto> DataPoints { get; set; } = new List<DataPointDto>();
    }
}
