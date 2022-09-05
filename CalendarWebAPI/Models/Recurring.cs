namespace CalendarWebAPI.Models
{
    public class Recurring
    {
        public Guid Id { get; set; }
        public string RecurringType { get; set; } = null!;
        public int? Separation { get; set; }
        public int? Gap { get; set; }

        public Recurring(Guid id, string reccuringType, int? separation, int? gap)
        {
            Id = id;
            RecurringType = reccuringType;
            Separation = separation;
            Gap = gap;
        }
    }
}
