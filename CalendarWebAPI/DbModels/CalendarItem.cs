using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class CalendarItem
    {
        public CalendarItem()
        {
            Schedulers = new HashSet<Scheduler>();
            Shifts = new HashSet<Shift>();
        }

        public Guid Id { get; set; }
        public Guid CalendarId { get; set; }
        public string? GivenName { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsHoliday { get; set; }
        public bool? IsWeekendday { get; set; }
        public bool? IsWorkingday { get; set; }
        public bool? IsMemorialday { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Calendar Calendar { get; set; } = null!;
        public virtual ICollection<Scheduler> Schedulers { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
