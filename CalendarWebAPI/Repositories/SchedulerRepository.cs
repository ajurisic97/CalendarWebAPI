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
            bool Filtered = true;
            if (dt1 == null || dt2 == null)
            {
                Filtered = false;
            }
            Expression<Func<Scheduler, Models.FullSchedulerItem>> result = scheduler => new Models.FullSchedulerItem()
            {
                SchedulersItems = !Filtered
                ? _calendarContext.SchedulerItems.Include(x=>x.CalendarItems).Where(x=>x.SchedulerId == scheduler.Id).Select(x=>SchedulerItemsMapper.FromDatabase(x)).ToList()
                : dt1 == null ? _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value<=dt2).OrderBy(x=>x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList()
                : dt2 == null ? _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value >= dt1).OrderBy(x => x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList()
                : _calendarContext.SchedulerItems.Include(x => x.CalendarItems).Where(x => x.SchedulerId == scheduler.Id && x.CalendarItems.Date.Value>=dt1 && x.CalendarItems.Date.Value <= dt2).OrderBy(x => x.CalendarItems.Date).Select(x => SchedulerItemsMapper.FromDatabase(x)).ToList(),
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
            return _calendarContext.Schedulers.Where(x => x.PersonId == id).Select(GetSchedulerProjection(dt,dt2));
        }


    }
}
