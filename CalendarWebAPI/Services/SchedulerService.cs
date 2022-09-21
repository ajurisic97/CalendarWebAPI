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
        public IEnumerable<PersonScheduler> GetPersonCalendars(List<Guid> personId, DateTime dt, DateTime dt2)
        {
            return _schedulerRepository.GetPersonCalendar(personId, dt, dt2);
        }
        
        public void Delete(List<Guid> ids)
        {
            _schedulerRepository.DeleteSchedulerItem(ids);
        }

        public void AddOnSaveChanges(List<Models.RecurringSchedulerItems> recurringSchedulerItems)
        {
            _schedulerRepository.AddOnSaveChanges(recurringSchedulerItems);
        }

        public void EditOnSaveChanges(List<RecurringSchedulerItems> recurringSchedulerItems)
        {
            _schedulerRepository.EditOnSaveChanges(recurringSchedulerItems);
        }
        // For now commented. Will be deleted if plan stays same:
        //public void EditPersonEvent(Guid personId, int eventType, string reccuringType, DateTime dt, Models.SchedulerItem schedulerItem)
        //{
        //    _schedulerRepository.EditPersonEvent(personId, eventType, reccuringType, dt, schedulerItem);
        //}
        //public async Task<ActionResult<IEnumerable<FullSchedulerItem>>> GetAll()
        //{
        //    return await _schedulerRepository.GetFull();
        //}

        //public Task<ActionResult<IEnumerable<FullSchedulerItem>>> GetByPerson(Guid id)
        //{
        //    return _schedulerRepository.GetByPersonId(id);
        //}
        //public IEnumerable<FullSchedulerItem> GetByDates(Guid id,DateTime dt, DateTime dt2)
        //{
        //    return  _schedulerRepository.GetByPersonAndDate(id,dt,dt2);
        //}



        // For now commented. Will be deleted if plan stays same:
        //public Models.SchedulerItem AddSchedulerItem(Guid SchedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        //{
        //    return _schedulerRepository.AddSchedulerItem(SchedulerId, dt, schedulerItem);
        //}

        //public void AddRecurringSchedulerItems(Guid personId, int eventType, Models.SchedulerItem schedulerItem, string? recurringType, DateTime? endDate)
        //{
        //    _schedulerRepository.AddRecurringItems(personId,eventType, schedulerItem,recurringType, endDate);
        //}
        //public void Edit(int eventType, string recurring,DateTime dt, Models.SchedulerItem schedulerItem)
        //{
        //    _schedulerRepository.EditSchedulerItem(eventType,recurring,dt, schedulerItem);
        //}



    }
}
