using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class SchedulerItemsMapper
    {
        public static SchedulerItem FromDatabase(DbModels.SchedulerItem sidb)
        {
            return new SchedulerItem(sidb.Id, sidb.StartTime, sidb.EndTime,sidb.CalendarItems.Date.Value);
        }

        public static DbModels.SchedulerItem ToDatabase(SchedulerItem si)
        {
            return null;
        }
    }
}
