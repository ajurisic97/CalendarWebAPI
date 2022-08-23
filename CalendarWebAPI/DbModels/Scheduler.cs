using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Scheduler
    {
        public Scheduler()
        {
            SchedulerItems = new HashSet<SchedulerItem>();
        }

        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid PersonId { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<SchedulerItem> SchedulerItems { get; set; }
    }
}
