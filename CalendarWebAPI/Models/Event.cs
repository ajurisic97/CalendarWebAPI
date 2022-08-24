namespace CalendarWebAPI.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public decimal Coefficient { get; set; }
        public Guid ReccuringId { get; set; }

        public virtual ICollection<Scheduler> Schedulers { get; set; }

        public Event(Guid id, string name, int type, decimal coefficient, Guid reccuringId)
        {
            Id = id;
            Name = name;
            Type = type;
            Coefficient = coefficient;
            ReccuringId = reccuringId;
        }
    }
}
