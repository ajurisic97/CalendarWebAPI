namespace CalendarWebAPI.Models
{
    public class PersonViewModel
    {
        public List<Person> People { get; set; }
        public int PeopleCount { get; set; }
        public PersonViewModel(List<Person> people, int peopleCount)
        {
            People = people;
            PeopleCount = peopleCount;
        }
    }
}
