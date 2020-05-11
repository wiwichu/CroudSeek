using System;

namespace CroudSeek.Shared
{
    public class QuestDto : BaseDto
    {
        public int ZoneId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
