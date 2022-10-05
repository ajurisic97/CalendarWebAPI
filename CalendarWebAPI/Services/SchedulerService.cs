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
        public IEnumerable<object> GetPersonCalendars(List<Guid> peopleIds, DateTime dt, DateTime dt2,string appName)
        {
            return _schedulerRepository.GetPersonCalendar(peopleIds, dt, dt2,appName);
        }

        public void AddOnSaveChanges(string appName,List<Models.RecurringSchedulerItems> recurringSchedulerItems)
        {
            _schedulerRepository.AddOnSaveChanges(appName,recurringSchedulerItems);
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
