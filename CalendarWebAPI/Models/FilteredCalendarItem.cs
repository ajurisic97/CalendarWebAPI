﻿namespace CalendarWebAPI.Models
{
    public class FilteredCalendarItem
    {
        public Guid? Id { get; set; }
      
        public DateTime? Date { get; set; }

        public bool? IsHoliday { get; set; }

        public bool? IsWeekendday { get; set; }


       public FilteredCalendarItem(Guid? id, DateTime? date, bool? isHoliday, bool? isWeekendday)
        {
            Id = id;
            Date = date;
            IsHoliday = isHoliday;
            IsWeekendday = isWeekendday;
        }
    }
}
