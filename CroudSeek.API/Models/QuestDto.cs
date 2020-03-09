using System;

namespace CroudSeek.API.Models
{
    public class QuestDto
    {
        public int Id { get; set; }
        public int Owner { get; set; }
        public int Zone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
