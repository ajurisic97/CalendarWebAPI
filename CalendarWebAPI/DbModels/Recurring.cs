using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Recurring
    {
        public Recurring()
        {
            Events = new HashSet<Event>();
        }

        public Guid Id { get; set; }
        public string RecurringType { get; set; } = null!;
        public int? Separation { get; set; }
        public int? Gap { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
