namespace CalendarWebAPI.Models
{
    public class PersonCalendar
    {
        public DateTime? Date;
        public decimal Coef;
        public int DayType;
        public TimeSpan? StartTime;
        public TimeSpan? EndTime;
        public PersonCalendar(DateTime? date, decimal coef, int dayType, TimeSpan? startTime, TimeSpan? endTime)
        {
            Date = date;
            Coef = coef;
            DayType = dayType;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
