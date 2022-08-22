using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Holiday
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? DateDay { get; set; }
        public string? DateMonth { get; set; }
        public bool? IsCommon { get; set; }
        public bool? IsPermanent { get; set; }
        public Guid? ConfessionId { get; set; }

        public virtual Confession? Confession { get; set; }
    }
}
