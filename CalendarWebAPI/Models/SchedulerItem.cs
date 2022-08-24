namespace CalendarWebAPI.Models
{
    public class SchedulerItem
    {
        public Guid Id { get; set; }
        public Scheduler Scheduler { get; set; }
        public CalendarItem CalendarItem { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        public SchedulerItem(Guid id, Scheduler scheduler, CalendarItem calendarItem, TimeOnly? startTime, TimeOnly? endTime)
        {
            Id = id;
            Scheduler = scheduler;
            CalendarItem = calendarItem;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
