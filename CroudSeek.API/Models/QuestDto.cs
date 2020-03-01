using System;

namespace CroudSeek.API.Models
{
    public class QuestDto
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public Guid Zone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
