using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Scheduler
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid CalendarItemsId { get; set; }
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public virtual CalendarItem CalendarItems { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
