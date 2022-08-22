using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Event
    {
        public Event()
        {
            Schedulers = new HashSet<Scheduler>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public decimal Coefficient { get; set; }

        public virtual ICollection<Scheduler> Schedulers { get; set; }
    }
}
