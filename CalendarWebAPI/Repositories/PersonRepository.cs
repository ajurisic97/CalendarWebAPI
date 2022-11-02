using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebAPI.Repositories
{
    public class PersonRepository
    {
        private readonly CalendarContext _calendarContext;
        public PersonRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }
        public Models.PersonViewModel GetPeopleByPage(string? filter, int pageNumber, int pageSize)
        {
            List<Person> peopleResponse;
            if (filter != null){
                peopleResponse = _calendarContext.People.Where(p => p.FirstName.ToLower().Contains(filter.ToLower()) || p.LastName.ToLower().Contains(filter.ToLower())).ToList();
            }
            else
            {
                peopleResponse = _calendarContext.People.ToList();
            }
            var countPeople = peopleResponse.Count();
            List<Models.Person> people = peopleResponse
                .OrderBy(x => x.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => PersonMapper.FromDatabase(x)).ToList();
            Models.PersonViewModel pvm = new Models.PersonViewModel(people, countPeople);
            return pvm;
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
            var dbApp = _calendarContext.Applications.Where(x => x.Name == "Scheduler").First();
            var dbEvents = _calendarContext.ApplicationEvents.Where(x => x.Application == dbApp).Select(x=>x.EventId).ToList();

            
            List<Scheduler> schedulers = new List<Scheduler>();
            foreach(var dbEvent in dbEvents)
            {
                schedulers.Add(new Scheduler
                {
                    Id = new Guid(),
                    PersonId = dbPerson.Id,
                    EventId = dbEvent
                });
            }
            _calendarContext.People.Add(dbPerson);
            _calendarContext.Schedulers.AddRange(schedulers);
            _calendarContext.SaveChanges();
            var result = _calendarContext.People.SingleOrDefault(x => x.FirstName == person.FirstName && x.LastName == person.LastName);
            return PersonMapper.FromDatabase(result);
        }

        public Models.Person Edit(Models.Person person)
        {
            var dbPerson = PersonMapper.ToDatabase(person);
            var x = _calendarContext.People.AsNoTracking().FirstOrDefault(p => p.Id == dbPerson.Id);
            dbPerson.RowVersion = x.RowVersion;
            _calendarContext.People.Update(dbPerson);
            _calendarContext.SaveChanges();
            var result = _calendarContext.People.SingleOrDefault(x => x.FirstName == person.FirstName && x.LastName == person.LastName);
            return PersonMapper.FromDatabase(result);
        }

        public void Delete(Guid personId)
        {
            var dbPerson = _calendarContext.People.Where(x => x.Id == personId).First();
            if(dbPerson != null)
            {
                // Kad promjenim bazu nadodati ovdje IsActive = false; te update persona (NE DELETE!!!!!)

            }
            

        }
    }
}
