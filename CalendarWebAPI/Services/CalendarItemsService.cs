

using CalendarWebAPI.Dtos;
using CalendarWebAPI.Models;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class CalendarItemsService
    {
        public CalendarItemsRepository _calendarItemsRepository;
        public CalendarItemsService(CalendarItemsRepository cir)
        {
            _calendarItemsRepository = cir;
        }

        public IEnumerable<FullCalendarDto> GetAll(DateTime? dt, DateTime? dt2,int depth=7)
        {
            return _calendarItemsRepository.GetCalendarsWithSubCalendars(dt,dt2,depth);
        }

        public Calendar Add(Calendar calendar)
        {
            return _calendarItemsRepository.AddCalendar(calendar);
        }

        public void Delete(Guid id)
        {
            _calendarItemsRepository.Delete(id);
        }

        public void Edit(Calendar calendar)
        {
            _calendarItemsRepository.Edit(calendar);
        }
    }
}
