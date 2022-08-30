using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class SchedulerItemsMapper
    {
        public static SchedulerItem FromDatabase(DbModels.SchedulerItem sidb)
        {
            return new SchedulerItem(sidb.Id,sidb.StartTime, sidb.EndTime,sidb.CalendarItems.Date.Value);
        }

        public static DbModels.SchedulerItem ToDatabase(Guid schedulerId,Guid calendarItemId,SchedulerItem si)
        {
            return new DbModels.SchedulerItem
            {
                Id = si.Id == null ? new Guid() : si.Id.Value,
                StartTime = si.StartTime,
                EndTime = si.EndTime,
                SchedulerId = schedulerId,
                CalendarItemsId = calendarItemId,

            };
        }
    }
}
