namespace CalendarWebAPI.Models
{
    public class PersonCalendar
    {
        public DateTime? Date;
        public decimal Coef;
        public int DayType;

        public PersonCalendar(DateTime? date, decimal coef, int dayType)
        {
            Date = date;
            Coef = coef;
            DayType = dayType;
        }
    }
}
