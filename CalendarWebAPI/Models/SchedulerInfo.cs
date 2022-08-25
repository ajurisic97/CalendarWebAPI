namespace CalendarWebAPI.Models
{
    public class SchedulerInfo
    {
        public SchedulerItem SchedulerItem { get; set; }
        public Guid SchedulerId { get; set; }

        public SchedulerInfo(SchedulerItem schedulerItem, Guid schedulerId)
        {
            SchedulerItem = schedulerItem;
            SchedulerId = schedulerId;
        }
    }
}
