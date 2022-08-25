namespace CalendarWebAPI.Models
{
    public class SchedulerItem
    {
        public Guid Id { get; set; }
        //public CalendarItem CalendarItem { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public SchedulerItem(Guid id, TimeSpan? startTime, TimeSpan? endTime, DateTime date)
        {
            Id = id;
            //CalendarItem = calendarItem;
            StartTime = startTime;
            EndTime = endTime;
            Date = date;
        }
    }
}
