using CalendarWebAPI.Models;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class SchedulerService
    {
        public SchedulerRepository _schedulerRepository;
        public SchedulerService(SchedulerRepository schedulerRepository)
        {
            _schedulerRepository = schedulerRepository;
        }

        public IEnumerable<Scheduler> GetAll()
        {
            return _schedulerRepository.GetSchedulers();
        }
    }
}
