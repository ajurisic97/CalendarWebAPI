using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class WorkingDay
    {
        public Guid Id { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuseday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool Sathurday { get; set; }
        public bool Sunday { get; set; }
    }
}
