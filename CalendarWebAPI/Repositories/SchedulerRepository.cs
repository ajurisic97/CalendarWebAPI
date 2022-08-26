using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CalendarWebAPI.Repositories
{
    public class SchedulerRepository
    {
        private readonly CalendarContext _calendarContext;
        public SchedulerRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public Expression<Func<Scheduler, Models.FullSchedulerItem>> GetSchedulerProjection(DateTime? dt1, DateTime? dt2)
        {
            bool firstExists = !(dt1 == DateTime.MinValue);
            bool secondExists = !(dt2 == DateTime.MinValue);
            Expression<Func<Scheduler, Models.FullSchedulerItem>> result = scheduler => new Models.FullSchedulerItem()
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

        public IEnumerable<Models.FullSchedulerItem> GetFull()
        {                                     
            var result = _calendarContext.Schedulers.Select(GetSchedulerProjection(null,null));
            return result.ToList();
        }

        public IEnumerable<Models.FullSchedulerItem> GetByPersonId(Guid id)
        {
            return _calendarContext.Schedulers.Where(x=>x.PersonId==id).Select(GetSchedulerProjection(null,null));
        }

        public IEnumerable<Models.FullSchedulerItem> GetByPersonAndDate(Guid id,DateTime dt, DateTime dt2)
        {
            var result = _calendarContext.Schedulers.Where(x => x.PersonId == id).Select(GetSchedulerProjection(dt,dt2)).ToList();
            return result;
        }

        public Models.SchedulerItem AddSchedulerItem(Guid schedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        {
            var calendarItem = _calendarContext.CalendarItems.Where(x=>x.Date == dt).FirstOrDefault();
            var dbScheduler = SchedulerItemsMapper.ToDatabase(schedulerId,calendarItem.Id,schedulerItem);
            _calendarContext.SchedulerItems.Add(dbScheduler);
            _calendarContext.SaveChanges();
            return schedulerItem;

        }

        public void EditSchedulerItem(Guid schedulerId,DateTime dt,Models.SchedulerItem schedulerItem)
        {
            var calendarItem = _calendarContext.CalendarItems.Where(x => x.Date == dt).FirstOrDefault();
            schedulerItem.Date = dt;
            var dbSchedulerItem = SchedulerItemsMapper.ToDatabase(schedulerId, calendarItem.Id, schedulerItem);
            _calendarContext.SchedulerItems.Update(dbSchedulerItem);
            _calendarContext.SaveChanges();
        }

        public void DeleteSchedulerItem(Guid id)
        {
            var schedulerItem = _calendarContext.SchedulerItems.FirstOrDefault(x=>x.Id == id);
            _calendarContext.Remove(schedulerItem);
            _calendarContext.SaveChanges();
        }


    }
}
