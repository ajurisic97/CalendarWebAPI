namespace CalendarWebAPI.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public decimal Coefficient { get; set; }
        public Guid RecurringId { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Scheduler> Schedulers { get; set; }

        public Event(Guid id, string name, int type, decimal coefficient, Guid reccuringId, string description)
        {
            Id = id;
            Name = name;
            Type = type;
            Coefficient = coefficient;
            RecurringId = reccuringId;
            Description = description;
        }
    }
}
