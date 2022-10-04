using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class ApplicationEvent
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid EventId { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
    }
}
