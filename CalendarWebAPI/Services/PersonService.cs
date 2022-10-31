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
        public Person GetPerson(Guid guid)
        {
            return _personRepository.GetById(guid);
        }
        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetPeople();
        }

        public Person Add(Person person)
        {
            return _personRepository.Add(person);
        }

        public Person Edit(Person person)
        {
            return _personRepository.Edit(person);
        }

        public void Delete(Guid guid)
        {
            _personRepository.Delete(guid);
        }
    }
}
