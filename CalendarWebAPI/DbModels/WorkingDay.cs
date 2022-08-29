using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class WorkingDay
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsWorkingDay { get; set; }
    }
}
