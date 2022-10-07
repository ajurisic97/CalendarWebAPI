using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class SchedulerItem
    {
        public Guid Id { get; set; }
        public Guid SchedulerId { get; set; }
        public Guid CalendarItemsId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Description { get; set; }
        public bool? CreatedByUser { get; set; }

        public virtual CalendarItem CalendarItems { get; set; } = null!;
        public virtual Scheduler Scheduler { get; set; } = null!;
    }
}
