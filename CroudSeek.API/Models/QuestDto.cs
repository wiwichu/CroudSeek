using System;

namespace CroudSeek.API.Models
{
    public class QuestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ZoneId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
