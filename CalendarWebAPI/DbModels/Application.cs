using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Application
    {
        public Application()
        {
            ApplicationEvents = new HashSet<ApplicationEvent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public virtual ICollection<ApplicationEvent> ApplicationEvents { get; set; }
    }
}
