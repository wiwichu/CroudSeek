using System;

namespace CroudSeek.Shared
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public int OwnerId { get; set; }
        public bool CanEdit { get; set; }
    }
}
