using CalendarWebAPI.Models;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class EventService
    {
        public EventRepository _eventRepository;
        public EventService(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> GetAll(string? applicationName="Scheduler")
        {
            return _eventRepository.GetEvents(applicationName);
        }
        public Event Add(string eventName,string eventDescription)
        {
            return _eventRepository.Add(eventName,eventDescription);
        }
        public void Delete(int eventType)
        {
            _eventRepository.Delete(eventType);
        }
        //JUST for adding uncomment
        /*EventRepository eventRepository = new EventRepository(_dbContext);
        eventRepository.AddEventsAndRecurrings();
        eventRepository.AddPerson();*/

        //public void AddEventsRecurringsAndPeople()
        //{
        //    _eventRepository.AddEventsAndRecurrings();
        //    _eventRepository.AddPerson(); 
        //}
    }
}
