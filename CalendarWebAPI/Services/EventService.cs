﻿using CalendarWebAPI.Models;
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

        public IEnumerable<Event> GetAll()
        {
            return _eventRepository.GetEvents();
        }
    }
}