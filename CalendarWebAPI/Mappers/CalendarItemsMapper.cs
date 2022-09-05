using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class CalendarItemsMapper
    {
        public static CalendarItem FromDbCalendarItems(DbModels.CalendarItem item)
        {
           return new CalendarItem(item.Id,null, item.GivenName, item.Date, item.IsHoliday, item.IsWeekendday, item.IsWorkingday, item.IsMemorialday, item.IsActive, item.IsApproved);
        }

        public static DbModels.CalendarItem ToDatabase(CalendarItem item)
        {
            return new DbModels.CalendarItem
            {
                Id = new Guid(),
                CalendarId = new Guid(),
                GivenName = item.GivenName,
                Date = item.Date,
                IsHoliday = item.IsHoliday,
                IsWeekendday = item.IsWeekendday,
                IsWorkingday = item.IsWorkingday,
                IsActive = item.IsActive,
                IsApproved = item.IsApproved,

            };
        }

        public static FilteredCalendarItem FilterData(CalendarItem calendarItem)
        {
            return new FilteredCalendarItem(calendarItem.Id, calendarItem.Date, calendarItem.IsHoliday, calendarItem.IsWeekendday, calendarItem.IsWorkingday);
        }
    }
}
