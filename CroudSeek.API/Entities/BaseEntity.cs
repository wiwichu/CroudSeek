using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CroudSeek.API.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
