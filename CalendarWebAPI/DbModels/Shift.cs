﻿using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Shift
    {
        public Guid Id { get; set; }
        public Guid CalendarItemId { get; set; }
        public string? Description { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? ShiftType { get; set; }
        public bool? IsActive { get; set; }

        public virtual CalendarItem CalendarItem { get; set; } = null!;
    }
}
