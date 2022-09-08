using CalendarWebAPI.Models;
using CalendarWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAPI.Services
{
    public class SchedulerService
    {
        public SchedulerRepository _schedulerRepository;
        public SchedulerService(SchedulerRepository schedulerRepository)
        {
            _schedulerRepository = schedulerRepository;
        }

        public async Task<ActionResult<IEnumerable<FullSchedulerItem>>> GetAll()
        {
            return await _schedulerRepository.GetFull();
        }

        public Task<ActionResult<IEnumerable<FullSchedulerItem>>> GetByPerson(Guid id)
        {
            return _schedulerRepository.GetByPersonId(id);
        }
        public IEnumerable<FullSchedulerItem> GetByDates(Guid id,DateTime dt, DateTime dt2)
        {
            return  _schedulerRepository.GetByPersonAndDate(id,dt,dt2);
        }

        public IEnumerable<PersonScheduler> GetPersonCalendars(List<Guid> personId,DateTime dt, DateTime dt2)
        {
            return _schedulerRepository.GetPersonCalendar(personId,dt,dt2);
        }
        public Models.SchedulerItem AddSchedulerItem(Guid SchedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        {
            return _schedulerRepository.AddSchedulerItem(SchedulerId, dt, schedulerItem);
        }
        
        public void AddRecurringSchedulerItems(Guid personId, int eventType, Models.SchedulerItem schedulerItem, string? recurringType, DateTime? endDate)
        {
            _schedulerRepository.AddRecurringItems(personId,eventType, schedulerItem,recurringType, endDate);
        }
        public void Edit(Guid schedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        {
            _schedulerRepository.EditSchedulerItem(schedulerId,dt, schedulerItem);
        }

        public void Delete(Guid id)
        {
            _schedulerRepository.DeleteSchedulerItem(id);
        }

    }
}
