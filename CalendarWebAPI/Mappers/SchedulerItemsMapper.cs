using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class SchedulerItemsMapper
    {
        public static SchedulerItem FromDatabase(DbModels.SchedulerItem sidb)
        {
            return new SchedulerItem(sidb.StartTime, sidb.EndTime,sidb.CalendarItems.Date.Value);
        }

        public static DbModels.SchedulerItem ToDatabase(Guid schedulerId,Guid calendarItemId,SchedulerItem si)
        {
            return new DbModels.SchedulerItem
            {
                StartTime = si.StartTime,
                EndTime = si.EndTime,
                SchedulerId = schedulerId,
                CalendarItemsId = calendarItemId,

            };
        }
    }
}
