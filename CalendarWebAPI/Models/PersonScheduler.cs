namespace CalendarWebAPI.Models
{
    public class PersonScheduler
    {
        public Guid Id { get; set; }
        public List<PersonCalendar> PersonCalendar { get; set; }

        public PersonScheduler(Guid personId,List<PersonCalendar> calendars)
        {
            Id = personId;
            PersonCalendar = calendars;
        }
    }
}
