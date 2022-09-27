using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class EventMapper
    {
        public static Event FromDatabase(DbModels.Event ev)
        {
            return new Event(ev.Id,ev.Name,ev.Type,ev.Coefficient,ev.RecurringId,ev.Description);
        }

        public static DbModels.Event ToDatabase(Event ev){
            return new DbModels.Event
            {
                Id = Guid.NewGuid(),
                Name = ev.Name,
                Type = ev.Type,
                Coefficient = ev.Coefficient,
                RecurringId = ev.RecurringId,
                Description = ev.Description
            };
        }
        
    }
}
