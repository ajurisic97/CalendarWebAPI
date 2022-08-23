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
        public string ReccuringType { get; set; } = null!;
        public int? Separation { get; set; }
        public int? NumOfOccurrences { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
