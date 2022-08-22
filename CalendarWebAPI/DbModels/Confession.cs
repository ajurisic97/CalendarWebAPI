using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Confession
    {
        public Confession()
        {
            Holidays = new HashSet<Holiday>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }
    }
}
