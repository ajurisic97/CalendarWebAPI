using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Event
    {
        public Event()
        {
            ApplicationEvents = new HashSet<ApplicationEvent>();
            Schedulers = new HashSet<Scheduler>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public decimal Coefficient { get; set; }
        public Guid RecurringId { get; set; }
        public string? Description { get; set; }

        public virtual Recurring Recurring { get; set; } = null!;
        public virtual ICollection<ApplicationEvent> ApplicationEvents { get; set; }
        public virtual ICollection<Scheduler> Schedulers { get; set; }
    }
}
