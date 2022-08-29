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
            List<Models.FullSchedulerItem> fsi = new List<Models.FullSchedulerItem>();
            //Grouping by name of event:
            foreach(var item in result)
            {
                var alreadyExists = fsi.Any(x => x.Name.Equals(item.Name));
                if (alreadyExists)
                {
                    fsi.Where(x => x.Name.Equals(item.Name)).First().SchedulersItems.AddRange(item.SchedulersItems);
                }
                else
                {
                    fsi.Add(item);
                }
            }
            //ako ne trebaju prazni nadodati: .Where(x=>x.SchedulersItems.Any())
            //eventualno filtracija i po imenu eventa .Where(x=>x.Name=="filteredName")
            //var test = result.Select(x=> new{ x.Name, x.Coef, x.SchedulersItems}).GroupBy(x => new { x.Name, x.Coef }).ToList();
            return fsi;
        }

        public Models.SchedulerItem AddSchedulerItem(Guid schedulerId,DateTime dt, Models.SchedulerItem schedulerItem)
        {
            var calendarItem = _calendarContext.CalendarItems.Where(x=>x.Date == dt).FirstOrDefault();
            var dbScheduler = SchedulerItemsMapper.ToDatabase(schedulerId,calendarItem.Id,schedulerItem);
            
            _calendarContext.SchedulerItems.Add(dbScheduler);
            _calendarContext.SaveChanges();
            return schedulerItem;

        }

        public void AddRecurringItems(Guid schedulerId,DateTime dt,Models.SchedulerItem schedulerItem, string typeOfRecurring,DateTime? EndDate)
        {
            var calendarItem = _calendarContext.CalendarItems.Where(x => x.Date == dt).FirstOrDefault();
            var occ = _calendarContext.Recurrings.Where(x => x.ReccuringType == typeOfRecurring).Select(x => x.NumOfOccurrences).FirstOrDefault();
            var currentDate = dt;
            List<SchedulerItem> schedulerItems = new List<SchedulerItem>();
            var dbScheduler = SchedulerItemsMapper.ToDatabase(schedulerId, calendarItem.Id, schedulerItem);
            schedulerItems.Add(dbScheduler);
            while ( currentDate.AddDays(occ.Value) < EndDate)
            {

                currentDate = currentDate.AddDays(occ.Value);
                calendarItem = _calendarContext.CalendarItems.Where(x => x.Date == currentDate).FirstOrDefault();
                dbScheduler = SchedulerItemsMapper.ToDatabase(schedulerId, calendarItem.Id, schedulerItem);
                schedulerItems.Add(dbScheduler);
            }
            _calendarContext.SchedulerItems.AddRange(schedulerItems);
            _calendarContext.SaveChanges();

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
