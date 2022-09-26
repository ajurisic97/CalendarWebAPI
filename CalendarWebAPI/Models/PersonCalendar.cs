namespace CalendarWebAPI.Models
{
    public class PersonCalendar
    {
        public Guid Id { get; set; }
        public DateTime? Date;
        public decimal Coef;
        public int DayType;
        public TimeSpan? StartTime;
        public TimeSpan? EndTime;
        public string Description;
        public PersonCalendar(Guid id, DateTime? date, decimal coef, int dayType, TimeSpan? startTime, TimeSpan? endTime, string description)
        {
            Id = id;
            Date = date;
            Coef = coef;
            DayType = dayType;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
        }
    }
}
