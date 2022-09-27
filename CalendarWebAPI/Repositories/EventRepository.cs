using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
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

        public IEnumerable<Models.Event> GetEvents()
        {
            return _dbContext.Events.Select(x => EventMapper.FromDatabase(x));
        }
        public Models.Event Add(string eventName, string eventDescription)
        {
            var dbRecurrings = _dbContext.Recurrings.Select(x => x.Id).ToList();
            var counter = _dbContext.Events.Max(x => x.Type);
            counter+= 1;
            List<Event> events = new List<Event>();
            foreach(var dbRec in dbRecurrings)
            {
                Event dbEvent = new Event
                {
                    Id = Guid.NewGuid(),
                    RecurringId = dbRec,
                    Name = eventName,
                    Description = eventDescription,
                    Type = counter,
                    Coefficient = Decimal.Parse("1,0")

                };
                events.Add(dbEvent);

            }
            _dbContext.Events.AddRange(events);
            _dbContext.SaveChanges();
            var result = _dbContext.Events.Where(x => x.Name == eventName).FirstOrDefault();
            return EventMapper.FromDatabase(result);
        }

        public void Delete(int eventType)
        {
            var events = _dbContext.Events.Where(x => x.Type.Equals(eventType));
            _dbContext.Events.RemoveRange(events);
            _dbContext.SaveChanges();
        }

        //public void AddRecurrings()
        //{
        //    _dbContext.SchedulerItems.RemoveRange(_dbContext.SchedulerItems);
        //    _dbContext.Schedulers.RemoveRange(_dbContext.Schedulers);
        //    _dbContext.Recurrings.RemoveRange(_dbContext.Recurrings);
        //    List<Recurring> listRecurrings = new List<Recurring>();
        //    string? path = AppDomain.CurrentDomain.BaseDirectory;
        //    using (StreamReader r = new StreamReader(path+"/JsonFiles/Recurring.json"))
        //    {
        //        var recurrings = r.ReadToEnd();
        //        listRecurrings = JsonSerializer.Deserialize<List<Recurring>>(recurrings);
        //    }
        //    _dbContext.Recurrings.AddRange(listRecurrings);
        //    _dbContext.SaveChanges();

        //}
        //public void AddEventsAndRecurrings()
        //{
        //    //EventRepository eventRepository = new EventRepository(_dbContext);
        //    //eventRepository.AddEventsAndRecurrings();
        //    _dbContext.Events.RemoveRange(_dbContext.Events);
        //    AddRecurrings();
        //    List<Event> listEvents = new List<Event>();
        //    string? path = AppDomain.CurrentDomain.BaseDirectory;
        //    using (StreamReader r = new StreamReader(path + "/JsonFiles/Event.json"))
        //    {
        //        var events = r.ReadToEnd();
        //        listEvents = JsonSerializer.Deserialize<List<Event>>(events);
        //    }
        //    var recurrings = _dbContext.Recurrings.ToList();
        //    List<Event> eventsAdd = new List<Event>();
        //    foreach(var e in listEvents)
        //    {
        //        foreach(var recurring in recurrings)
        //        {
        //            Event ev = new Event()
        //            {
        //                Id = new Guid(),
        //                Coefficient=e.Coefficient,
        //                Name=e.Name,
        //                Type=e.Type,
        //                RecurringId=recurring.Id,
        //                Description =e.Description

        //            };

        //            eventsAdd.Add(ev);
        //        }
        //    }

        //    _dbContext.Events.AddRange(eventsAdd);
        //    _dbContext.SaveChanges();

        //}

        //public void AddPerson()
        //{

        //    _dbContext.SchedulerItems.RemoveRange(_dbContext.SchedulerItems);
        //    _dbContext.Schedulers.RemoveRange(_dbContext.Schedulers);
        //    _dbContext.People.RemoveRange(_dbContext.People);
        //    List<Person> listPeople = new List<Person>();
        //    string? path = AppDomain.CurrentDomain.BaseDirectory;
        //    using (StreamReader r = new StreamReader(path + "/JsonFiles/Person.json"))
        //    {
        //        var peopleJson = r.ReadToEnd();
        //        listPeople = JsonSerializer.Deserialize<List<Person>>(peopleJson);
        //    }
        //    var events = _dbContext.Events.ToList();
        //    List<Scheduler> schedulers = new List<Scheduler>();
        //    foreach(var e in events)
        //    {
        //        foreach(var p in listPeople)
        //        {
        //            Scheduler sched = new Scheduler()
        //            {
        //                Id = new Guid(),
        //                EventId = e.Id,
        //                PersonId = p.Id
        //            };
        //            schedulers.Add(sched);
        //        }
        //    }
        //    _dbContext.People.AddRange(listPeople);
        //    _dbContext.Schedulers.AddRange(schedulers);
        //    _dbContext.SaveChanges();
        //    //adding Scheduler after adding Person (And for that we first need Recurring and then Event)
        //}
    }
}
