namespace CalendarWebAPI.Models
{
    public class Scheduler
    {
        public Guid Id { get; set; }
        public Event Event { get; set; }
        public Person Person { get; set; }

        public virtual ICollection<SchedulerItem> SchedulerItems { get; set; }

        public Scheduler(Guid id, Event @event, Person person)
        {
            Id = id;
            Event = @event;
            Person = person;
        }
    }
}
