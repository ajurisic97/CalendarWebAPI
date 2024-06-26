﻿using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using System.Text.Json;

namespace CalendarWebAPI.Repositories
{
    public class EventRepository
    {
        private readonly CalendarContext _dbContext;
        public EventRepository(CalendarContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Models.Event> GetEvents(string? applicationName="Scheduler")
        {

            var appEvents = _dbContext.ApplicationEvents.Where(x => x.Application.Name == applicationName).Select(ae=>ae.EventId);
            return _dbContext.Events.Where(x=>appEvents.Contains(x.Id)).Select(x => EventMapper.FromDatabase(x));
        }
        public Models.Event Add(string appName,string eventName, string eventDescription)
        {
            var dbRecurrings = _dbContext.Recurrings.Select(x => x.Id).ToList();
            var counter = _dbContext.Events.Max(x => x.Type);
            counter+= 1;
            List<Event> events = new List<Event>();
            List<ApplicationEvent> appEvents = new List<ApplicationEvent>();
            var appId = _dbContext.Applications.Where(x => x.Name == appName).Select(x => x.Id).FirstOrDefault();
            foreach (var dbRec in dbRecurrings)
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
                appEvents.Add(new ApplicationEvent()
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = appId,
                    EventId = dbEvent.Id,
                });
                events.Add(dbEvent);

            }
            _dbContext.ApplicationEvents.AddRange(appEvents);
            _dbContext.Events.AddRange(events);
            List<Scheduler> schedulers = new List<Scheduler>();
            var listPeople = _dbContext.People.ToList();
            foreach (var e in events)
            {
                foreach (var p in listPeople)
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
            _dbContext.Schedulers.AddRange(schedulers);
            _dbContext.SaveChanges();
            var result = _dbContext.Events.Where(x => x.Name == eventName).FirstOrDefault();
            return EventMapper.FromDatabase(result);
        }

        public void Delete(int eventType)
        {
            var events = _dbContext.Events.Where(x => x.Type.Equals(eventType)).ToList();
            var scheduler = _dbContext.Schedulers.Where(x => events.Contains(x.Event)).ToList();
            var schedulerItems = _dbContext.SchedulerItems.Where(x => scheduler.Contains(x.Scheduler));
            _dbContext.SchedulerItems.RemoveRange(schedulerItems);
            _dbContext.Schedulers.RemoveRange(scheduler);
            _dbContext.Events.RemoveRange(events);
            _dbContext.SaveChanges();
        }

        
    }
}
