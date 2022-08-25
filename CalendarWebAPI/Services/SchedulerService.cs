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

        public IEnumerable<FullSchedulerItem> GetAll()
        {
            return _schedulerRepository.GetFull();
        }

        public IEnumerable<FullSchedulerItem> GetByPerson(Guid id)
        {
            return _schedulerRepository.GetByPersonId(id);
        }
        public IEnumerable<FullSchedulerItem> GetByDates(Guid id,DateTime dt, DateTime dt2)
        {
            return _schedulerRepository.GetByPersonAndDate(id,dt,dt2);
        }

        public Models.SchedulerItem AddSchedulerItem(Guid SchedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        {
            return _schedulerRepository.AddSchedulerItem(SchedulerId, dt, schedulerItem);
        }


    }
}
