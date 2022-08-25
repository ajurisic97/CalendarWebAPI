using CalendarWebAPI.Models;

namespace CalendarWebAPI.Mappers
{
    public class FullScheduler
    {
        public static FullSchedulerItem FromDatabase(DbModels.SchedulerItem schedulerItem)
        {
            //return new FullSchedulerItem(
            //    schedulerItem.Scheduler.Id,
            //    schedulerItem.CalendarItems.Date,
            //    schedulerItem.CalendarItems.IsWorkingday


            //);
            return null;
        }

        public static DbModels.SchedulerItem ToDatabase(SchedulerItem schedulerItem)
        {
            return new DbModels.SchedulerItem
            {

                

            };
        }

        
    }
}
