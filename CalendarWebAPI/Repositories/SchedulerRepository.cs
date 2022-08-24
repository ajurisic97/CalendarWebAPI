using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebAPI.Repositories
{
    public class SchedulerRepository
    {
        private readonly CalendarContext _calendarContext;
        public SchedulerRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public IEnumerable<Models.Scheduler> GetSchedulers()
        {
            return _calendarContext.Schedulers.Include(x => x.Person)
                .Include(x => x.Event)
                .ThenInclude(x => x.Reccuring)
                .Select(x => SchedulerMapper.FromDatabase(x));
        }
    }
}
