using LifeIsShort.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeIsShort.Domain.Entities.Timelines
{
    public class Timeline : BaseEntity
    {
        public int MaxYears { get; set; }

        public string UserId { get; set; }

        public DateTime Birthday { get; set; }

        public TimelineType Type { get; set; }
    }
}
