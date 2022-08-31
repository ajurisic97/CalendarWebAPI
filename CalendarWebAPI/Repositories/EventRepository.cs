using CalendarWebAPI.DbModels;
using System.Text.Json;

namespace CalendarWebAPI.Repositories
{
    public class EventRepository
    {
        //REPO FOR AUTOMATICALLY ADDING RECURRINGS AND EVENTS
        private readonly CalendarContext _dbContext;
        public EventRepository(CalendarContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void AddRecurrings()
        {
            _dbContext.Recurrings.RemoveRange(_dbContext.Recurrings);
            List<Recurring> listRecurrings = new List<Recurring>();
            string? path = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r = new StreamReader(path+"/JsonFiles/Recurring.json"))
            {
                var recurrings = r.ReadToEnd();
                listRecurrings = JsonSerializer.Deserialize<List<Recurring>>(recurrings);
            }
            _dbContext.Recurrings.AddRange(listRecurrings);
            _dbContext.SaveChanges();
            
        }
        public void AddEventsAndRecurrings()
        {
            //EventRepository eventRepository = new EventRepository(_dbContext);
            //eventRepository.AddEventsAndRecurrings();
            _dbContext.Events.RemoveRange(_dbContext.Events);
            AddRecurrings();
            List<Event> listEvents = new List<Event>();
            string? path = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r = new StreamReader(path + "/JsonFiles/Event.json"))
            {
                var events = r.ReadToEnd();
                listEvents = JsonSerializer.Deserialize<List<Event>>(events);
            }
            var recurrings = _dbContext.Recurrings.ToList();
            List<Event> eventsAdd = new List<Event>();
            foreach(var e in listEvents)
            {
                foreach(var recurring in recurrings)
                {
                    Event ev = new Event()
                    {
                        Id = new Guid(),
                        Coefficient=e.Coefficient,
                        Name=e.Name,
                        ReccuringId=recurring.Id

                    };
                    
                    eventsAdd.Add(ev);
                }
            }
            
            _dbContext.Events.AddRange(eventsAdd);
            _dbContext.SaveChanges();
            
        }

        public void AddPerson()
        {
            AddEventsAndRecurrings();
            _dbContext.SchedulerItems.RemoveRange(_dbContext.SchedulerItems);
            _dbContext.Schedulers.RemoveRange(_dbContext.Schedulers);
            _dbContext.People.RemoveRange(_dbContext.People);
            List<Person> listPeople = new List<Person>();
            string? path = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamReader r = new StreamReader(path + "/JsonFiles/Person.json"))
            {
                var peopleJson = r.ReadToEnd();
                listPeople = JsonSerializer.Deserialize<List<Person>>(peopleJson);
            }
            var events = _dbContext.Events.ToList();
            List<Scheduler> schedulers = new List<Scheduler>();
            foreach(var e in events)
            {
                foreach(var p in listPeople)
                {
                    Scheduler sched = new Scheduler()
                    {
                        Id = new Guid(),
                        EventId = e.Id,
                        PersonId = p.Id
                    };
                    schedulers.Add(sched);
                }
            }
            _dbContext.People.AddRange(listPeople);
            _dbContext.Schedulers.AddRange(schedulers);
            _dbContext.SaveChanges();
            //adding Scheduler after adding Person (And for that we first need Recurring and then Event)
        }
    }
}
