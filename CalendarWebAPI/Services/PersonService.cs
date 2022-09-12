using CalendarWebAPI.Models;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class PersonService
    {
        public PersonRepository _personRepository;
        public PersonService(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetPeople();
        }

        public Person Add(Person person)
        {
            return _personRepository.Add(person);
        }
    }
}
