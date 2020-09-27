using LifeIsShort.Domain.Enums;
using System;

namespace LifeIsShort.Models.Timelines
{
    public class TimelineInputModel
    {
        public int MaxYears { get; set; }
        public DateTime Birthday { get; set; }
        public TimelineType Type { get; set; }
    }
}
