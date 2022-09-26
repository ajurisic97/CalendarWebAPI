using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class EventMapper
    {
        public static Event FromDatabase(DbModels.Event ev)
        {
            return new Event(ev.Id,ev.Name,ev.Type,ev.Coefficient,ev.RecurringId,ev.Description);
        }
    }
}
