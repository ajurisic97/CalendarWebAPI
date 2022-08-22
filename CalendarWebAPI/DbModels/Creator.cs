using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Creator
    {
        public Creator()
        {
            Calendars = new HashSet<Calendar>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Calendar> Calendars { get; set; }
    }
}
