using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CalendarWebAPI.Repositories
{
    public class SchedulerRepository
    {
        private readonly CalendarContext _calendarContext;
        private readonly CalendarItemsRepository _calendarItemsRepository;
        public SchedulerRepository(CalendarContext calendarContext, CalendarItemsRepository calendarItemsRepository)
        {
            _calendarContext = calendarContext;
            _calendarItemsRepository = calendarItemsRepository;
        }

        public Expression<Func<Scheduler, Models.FullSchedulerItem>> GetSchedulerProjection(DateTime? dt1, DateTime? dt2)
        {
            bool firstExists = !(dt1 == DateTime.MinValue);
            bool secondExists = !(dt2 == DateTime.MinValue);
            Expression<Func<Scheduler, Models.FullSchedulerItem>> result =   scheduler =>  new Models.FullSchedulerItem()
            {
                SchedulersItems = (!firstExists && !secondExists)
                ? _calendarContext.SchedulerItems.Include(x=>x.CalendarItems).Where(x=>x.SchedulerId == scheduler.Id).Select(x=>SchedulerItemsMapper.FromDatabase(x)).ToList()
                : (firstExists && secondExists) ? _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value >= dt1 && x.CalendarItems.Date.Value <= dt2).OrderBy(x => x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList()
                : secondExists ? _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value<=dt2.Value).OrderBy(x=>x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList()
                :  _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value >= dt1).OrderBy(x => x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList(),
                
                Name = scheduler.Event.Name,
                Coef = scheduler.Event.Coefficient
            };
            return result;
        }

        public IEnumerable<object> GetPersonCalendar(List<Guid> personIds,DateTime dt, DateTime dt2, bool forScheduler,string appName)
        {
            List<Models.PersonScheduler> personSchedulers = new List<Models.PersonScheduler>();
            List<Models.PersonPayRollScheduler> personPayRollSchedulers = new List<Models.PersonPayRollScheduler>();

            if (dt2 == DateTime.MinValue)
            {
                dt2 = DateTime.MaxValue;
            }
            var eventIds = _calendarContext.ApplicationEvents.Where(a => a.Application.Name == appName).Select(e => e.Id).ToList();
            foreach (var personId in personIds)
            {
                if (forScheduler)
                {
                    var schedulerItems = _calendarContext.SchedulerItems.Include(x => x.CalendarItems)
                                                                    .Include(x => x.Scheduler).ThenInclude(x => x.Event).ThenInclude(x => x.Recurring)
                                                                    .Include(x => x.Scheduler).ThenInclude(x => x.Person)
                                                                    .Where(x => x.Scheduler.PersonId == personId
                                                                    && x.CalendarItems.Date >= dt && x.CalendarItems.Date <= dt2 && eventIds.Contains(x.Scheduler.EventId))
                                                                    .Select(x => SchedulerItemsMapper.ToPersonCalendar(x));
                    personSchedulers.Add(new Models.PersonScheduler(personId, schedulerItems.ToList()));
                }
                else
                {
                    // if it is for payroll project we have filter by type as well and we dont need some properties (like Description) for payroll models
                    var schedulerItems = _calendarContext.SchedulerItems.Include(x => x.CalendarItems)
                                                                    .Include(x => x.Scheduler).ThenInclude(x => x.Event).ThenInclude(x => x.Recurring)
                                                                    .Include(x => x.Scheduler).ThenInclude(x => x.Person)
                                                                    .Where(x => x.Scheduler.PersonId == personId
                                                                    && x.CalendarItems.Date >= dt && x.CalendarItems.Date <= dt2 && eventIds.Contains(x.Scheduler.EventId))
                                                                    .Select(x => SchedulerItemsMapper.ToPersonCalendarPayRoll(x));
                    personPayRollSchedulers.Add(new Models.PersonPayRollScheduler(personId, schedulerItems.ToList()));
                }
                
                                                                    
                
            }
            if (forScheduler)
                return personSchedulers;
            else
                return personPayRollSchedulers;
        }

        public void AddOnSaveChanges(List<Models.RecurringSchedulerItems> listSchedulers)
        {
            List<SchedulerItem> schedulerItems = new List<SchedulerItem>();
            foreach (Models.RecurringSchedulerItems rsi in listSchedulers){
                var eventType = rsi.EventType;
                var typeOfRecurring = rsi.TypeOfRecurring;
                var recurring = _calendarContext.Recurrings.Where(r => r.RecurringType == typeOfRecurring).FirstOrDefault();

                var ev = typeOfRecurring == null ? _calendarContext.Events.Where(e => e.Type.Equals(eventType)).FirstOrDefault()
                                                      : _calendarContext.Events.Where(e => e.RecurringId.Equals(recurring.Id) && e.Type.Equals(eventType)).FirstOrDefault();
                if (rsi.SchedulerItem.Description.Trim().Length == 0)
                {
                    rsi.SchedulerItem.Description = ev.Description;
                }
                var schedulerItem = rsi.SchedulerItem;
                var dt = schedulerItem.Date;
                
                
                var personId = rsi.PersonId;
                var calendarItem = _calendarItemsRepository.GetCalendarItemsWithSubCulendar(dt, dt).FirstOrDefault();
                

                var eventId = ev.Id;
                
                var schedulerId = _calendarContext.Schedulers.Where(s => s.PersonId.Equals(personId) && s.EventId.Equals(eventId)).Select(s => s.Id).FirstOrDefault();                
                var dbScheduler = SchedulerItemsMapper.ToDatabase(schedulerId, (Guid)calendarItem.Id, schedulerItem);
                dbScheduler.Id = Guid.NewGuid();
                schedulerItems.Add(dbScheduler);
            }
            _calendarContext.SchedulerItems.AddRange(schedulerItems);
            _calendarContext.SaveChanges();

        }

        public void EditOnSaveChanges(List<Models.RecurringSchedulerItems> listSchedulers)
        {
            List<SchedulerItem> schedulerItems = new List<SchedulerItem>();
            foreach (Models.RecurringSchedulerItems rsi in listSchedulers)
            {
                var schedulerItem = rsi.SchedulerItem;
                var eventType = rsi.EventType;
                var recurring = "None";
                var personId = rsi.PersonId;
                var calendarItem = _calendarContext.CalendarItems.Where(x => x.Date == schedulerItem.Date).FirstOrDefault();
                var eventId = _calendarContext.Events.Where(x => x.Type == eventType && x.Recurring.RecurringType == recurring).Select(x => x.Id).FirstOrDefault();
                var schedulerId = _calendarContext.Schedulers.Where(x => x.EventId == eventId && x.PersonId == personId).Select(x => x.Id).FirstOrDefault(); 
                var dbSchedulerItem = SchedulerItemsMapper.ToDatabase(schedulerId, calendarItem.Id, schedulerItem);
                _calendarContext.SchedulerItems.Update(dbSchedulerItem);
                
            }
            _calendarContext.SaveChanges();
        }

        public void DeleteSchedulerItem(List<Guid> ids)
        {
            List<SchedulerItem> schedulerItems = new List<SchedulerItem>();
            foreach(Guid id in ids)
            {
                var schedulerItem = _calendarContext.SchedulerItems.FirstOrDefault(x => x.Id == id);
                if (schedulerItem != null)
                {
                    schedulerItems.Add(schedulerItem);
                }
                
            }
            
            _calendarContext.SchedulerItems.RemoveRange(schedulerItems);
            _calendarContext.SaveChanges();
            

        }

        
    }
}
