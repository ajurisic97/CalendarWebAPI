using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CalendarWebAPI.Models
{
    public class Calendar
    {
        public Guid? Id { get; set; }
        public Creator Creator { get; set; }
        public Calendar Paent { get; set; }

        public virtual ICollection<Calendar> SubCalendar { get; set; }
        public int Year { get; set; }

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsApproved { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public Calendar(Guid? id, Calendar parent, Creator creator, int year, string description, DateTime? startDate, DateTime? endDate, DateTime? createdDate, bool? isApproved)
        {
            Id = id;
            Paent = parent;
            Creator = creator;
            Year = year;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
            IsApproved = isApproved;

        }
    }
}
