namespace CalendarWebAPI.Models
{
    public class Recurring
    {
        public Guid Id { get; set; }
        public string ReccuringType { get; set; } = null!;
        public int? Separation { get; set; }
        public int? NumOfOccurrences { get; set; }

        public Recurring(Guid id, string reccuringType, int? separation, int? numOfOccurrences)
        {
            Id = id;
            ReccuringType = reccuringType;
            Separation = separation;
            NumOfOccurrences = numOfOccurrences;
        }
    }
}
