namespace CalendarWebAPI.Models
{
    public class RecurringSchedulerItems
    {
        
        public Guid PersonId { get; set; }
        public int EventType { get; set; }
        public SchedulerItem SchedulerItem { get; set; }
        public string? TypeOfRecurring { get; set; }
        public DateTime? EndDate { get; set; }
        public RecurringSchedulerItems(Guid personId, int eventType, SchedulerItem schedulerItem, string? typeOfRecurring, DateTime? endDate)
        {
            PersonId = personId;
            EventType = eventType;
            SchedulerItem = schedulerItem;
            TypeOfRecurring = typeOfRecurring;
            EndDate = endDate;
        }
    }
}
