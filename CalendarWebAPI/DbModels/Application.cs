using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Application
    {
        public Application()
        {
            Events = new HashSet<Event>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
