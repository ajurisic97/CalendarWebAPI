using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class CalendarItemsMapper
    {
        public static CalendarItem FromDbCalendarItems(DbModels.CalendarItem item)
        {
           return new CalendarItem(item.Id,null, item.GivenName, item.Date, item.IsHoliday, item.IsWeekendday, item.IsWorkingday, item.IsMemorialday, item.IsActive, item.IsApproved);
        }
    }
}
