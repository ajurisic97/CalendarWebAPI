namespace CalendarWebAPI.Models
{
    public class SchedulerItem
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public string? Description { get; set; }
        public bool CreatedByUser { get; set; }

        public SchedulerItem(Guid? id,TimeSpan? startTime, TimeSpan? endTime, DateTime date, string? description, bool createdByUser)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Date = date;
            Description = description;
            CreatedByUser = createdByUser;
        }
    }
}
