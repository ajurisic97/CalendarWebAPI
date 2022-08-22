namespace CalendarWebAPI.Models
{
    public class CalendarItem
    {
        public Guid? Id { get; set; }
        public Calendar Calendar { get; set; }
        public string GivenName { get; set; }
        public DateTime? Date { get; set; }

        public bool? IsHoliday { get; set; }

        public bool? IsWeekendday { get; set; }

        public bool? IsWorkingday { get; set; }

        public bool? IsMemorialday { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsApproved { get; set; }

        public CalendarItem(Guid? id, Calendar calendar, string givenName, DateTime? date, bool? isHoliday, bool? isWeekendday, bool? isWorkingday, bool? isMemorialday, bool? isActive, bool? isApproved)
        {
            Id = id;
            Calendar = calendar;
            GivenName = givenName;
            Date = date;
            IsHoliday = isHoliday;
            IsWeekendday = isWeekendday;
            IsWorkingday = isWorkingday;
            IsMemorialday = isMemorialday;
            IsActive = isActive;
            IsApproved = isApproved;
        }
    }
}
