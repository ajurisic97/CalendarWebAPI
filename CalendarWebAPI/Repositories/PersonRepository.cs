using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;

namespace CalendarWebAPI.Repositories
{
    public class PersonRepository
    {
        private readonly CalendarContext _calendarContext;
        public PersonRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public Models.Person GetById(Guid guid)
        {
            return _calendarContext.People.Where(x => x.Id == guid).Select(x=>PersonMapper.FromDatabase(x)).FirstOrDefault();
        }
        public IEnumerable<Models.Person> GetPeople()
        {
            return _calendarContext.People.Select(x => PersonMapper.FromDatabase(x));
        }

        public Models.Person Add(Models.Person person)
        {
            var dbPerson = PersonMapper.ToDatabase(person);
            _calendarContext.People.Add(dbPerson);
            _calendarContext.SaveChanges();
            var result = _calendarContext.People.SingleOrDefault(x => x.FirstName == person.FirstName && x.LastName == person.LastName);
            return PersonMapper.FromDatabase(result);
        }
    }
}
