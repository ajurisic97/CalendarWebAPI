namespace CalendarWebAPI.Models
{
    public class SchedulerItem
    {
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public SchedulerItem(TimeSpan? startTime, TimeSpan? endTime, DateTime date)
        {
            StartTime = startTime;
            EndTime = endTime;
            Date = date;
        }
    }
}
