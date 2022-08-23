using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class FullCalendarMapper
    {
        public static CalendarItem FromDatabase(DbModels.CalendarItem calendarItem)
        {
            return new CalendarItem(calendarItem.Id, CalendarMapper.FromDatabase(calendarItem.Calendar), calendarItem.GivenName,
                calendarItem.Date, calendarItem.IsHoliday, calendarItem.IsWeekendday, calendarItem.IsWorkingday,
                calendarItem.IsMemorialday, calendarItem.IsActive, calendarItem.IsApproved);
        }

        public static DbModels.CalendarItem ToDatabase(CalendarItem calendarItem)
        {
            return new DbModels.CalendarItem
            {

                GivenName = calendarItem.GivenName,
                CalendarId = calendarItem.Calendar.Id.Value,
                Date = calendarItem.Date,
                IsHoliday = calendarItem.IsHoliday,
                IsWeekendday = calendarItem.IsWeekendday,
                IsWorkingday = calendarItem.IsWorkingday,
                IsMemorialday = calendarItem.IsMemorialday,
                IsActive = calendarItem.IsActive,
                IsApproved = calendarItem.IsApproved

            };
        }
    }
}
