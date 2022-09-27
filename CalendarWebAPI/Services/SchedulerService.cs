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
        public IEnumerable<object> GetPersonCalendars(List<Guid> personId, DateTime dt, DateTime dt2, bool forScheduler)
        {
            return _schedulerRepository.GetPersonCalendar(personId, dt, dt2, forScheduler);
        }

        public void AddOnSaveChanges(List<Models.RecurringSchedulerItems> recurringSchedulerItems)
        {
            _schedulerRepository.AddOnSaveChanges(recurringSchedulerItems);
        }

        public void EditOnSaveChanges(List<RecurringSchedulerItems> recurringSchedulerItems)
        {
            _schedulerRepository.EditOnSaveChanges(recurringSchedulerItems);
        }

        public void Delete(List<Guid> ids)
        {
            _schedulerRepository.DeleteSchedulerItem(ids);
        }



    }
}
