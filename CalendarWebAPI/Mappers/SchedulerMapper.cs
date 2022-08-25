using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class SchedulerMapper
    {
        public static Scheduler FromDatabase(DbModels.Scheduler scheduler)
        {
            return new Scheduler(scheduler.Id, EventMapper.FromDatabase(scheduler.Event), PersonMapper.FromDatabase(scheduler.Person));
        }


    }
}
