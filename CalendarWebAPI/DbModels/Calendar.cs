using System;
using System.Collections.Generic;

namespace CalendarWebAPI.DbModels
{
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarItems = new HashSet<CalendarItem>();
            InverseParent = new HashSet<Calendar>();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? CreatorId { get; set; }
        public int Year { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsApproved { get; set; }
        public byte[]? RowVersion { get; set; }

        public virtual Creator? Creator { get; set; }
        public virtual Calendar? Parent { get; set; }
        public virtual ICollection<CalendarItem> CalendarItems { get; set; }
        public virtual ICollection<Calendar> InverseParent { get; set; }
    }
}
