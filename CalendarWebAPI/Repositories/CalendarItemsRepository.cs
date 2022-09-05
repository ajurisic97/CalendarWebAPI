using CalendarWebAPI.DbModels;
using CalendarWebAPI.Dtos;
using CalendarWebAPI.Mappers;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CalendarWebAPI.Repositories
{
    public class CalendarItemsRepository
    {
        private readonly CalendarContext _dbContext;
        public CalendarItemsRepository(CalendarContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Expression<Func<Calendar, FullCalendarDto>> GetCalendarProjection(DateTime? dt, DateTime? dt2, int maxDepth, int currentDepth = 0)
        {
            if(maxDepth <= 0)
            {
                maxDepth = 1;
            }
            currentDepth++;
            bool firstExists = !(dt == DateTime.MinValue);
            bool secondExists = !(dt2 == DateTime.MinValue);
            Expression<Func<Calendar, FullCalendarDto>> result = calendar => new FullCalendarDto()
            {
                CalendarId = calendar.Id,
                Description = calendar.Description,
                StartDate = calendar.StartDate,
                EndDate = calendar.EndDate,
                CalendarItems = (!firstExists && !secondExists)
                ? _dbContext.CalendarItems.Where(x => x.CalendarId == calendar.Id).OrderBy(x => x.Date).Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                : (secondExists && firstExists) ? _dbContext.CalendarItems.Where(x => x.CalendarId == calendar.Id && x.Date.Value >= dt && x.Date.Value <= dt2).OrderBy(x => x.Date).Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                : secondExists ? _dbContext.CalendarItems.Where(x => x.CalendarId == calendar.Id && x.Date.Value <= dt2).OrderBy(x => x.Date).Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                :  _dbContext.CalendarItems.Where(x => x.CalendarId == calendar.Id && x.Date.Value >= dt).OrderBy(x => x.Date).Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList(),
                SubCalendars = currentDepth == maxDepth
                ? new List<FullCalendarDto>()
                : calendar.InversePaent.AsQueryable().Select(GetCalendarProjection(dt, dt2, maxDepth, currentDepth)).ToList(),
                

            };
            return result;
        }

        public List<Models.CalendarItem> GetReccursiveItems(DateTime? dt, DateTime? dt2, Guid idParent, List<Models.CalendarItem> ci)
        {
            bool firstExists = !(dt == DateTime.MinValue);
            bool secondExists = !(dt2 == DateTime.MinValue);

            var calendarItems = (!firstExists && !secondExists)
               ? _dbContext.CalendarItems.Where(x => x.Calendar.Id == idParent)
                                            .OrderBy(x => x.Date)
                                            .Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                : (secondExists && firstExists) ? _dbContext.CalendarItems.Where(x => x.Calendar.Id == idParent && x.Date >= dt && x.Date <= dt2)
                                                                            .OrderBy(x=>x.Date)
                                                                           .Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                : secondExists ? _dbContext.CalendarItems.Where(x => x.Calendar.Id == idParent && x.Date.Value <= dt2)
                                                         .OrderBy(x => x.Date)
                                                         .Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList()
                : _dbContext.CalendarItems.Where(x => x.Calendar.Id == idParent && x.Date.Value >= dt)
                                           .OrderBy(x => x.Date)
                                           .Select(x => CalendarItemsMapper.FromDbCalendarItems(x)).ToList();

            var newDates = calendarItems.Select(x => x.Date).ToList();
            //var AlreadyExisting = ci.Where(x => newDates.Contains(x.Date.Value)).ToList();
            ci.RemoveAll(x => newDates.Contains(x.Date.Value));
            ci.AddRange(calendarItems);
            var child = _dbContext.Calendars.Where(x => x.PaentId == idParent).Select(x => x.Id).FirstOrDefault();
            if(child!=Guid.Empty)
            GetReccursiveItems(dt, dt2, child, ci);
            return ci.OrderBy(x=>x.Date).ToList();

        }

        public List<FullCalendarDto> GetCalendarsWithSubCalendars(DateTime? dt, DateTime? dt2, int depth=7)
        {
            //JUST for adding uncomment
            /*EventRepository eventRepository = new EventRepository(_dbContext);
            eventRepository.AddEventsAndRecurrings();
            eventRepository.AddPerson();*/
            var result = _dbContext.Calendars.Where(c => c.Paent == null).Select(GetCalendarProjection(dt, dt2, depth, 0));
            var calendarItems = _dbContext.Calendars.Where(x => x.InversePaent == null).Select(x => x.CalendarItems);
            return result.ToList();
        }


        public List<Models.FilteredCalendarItem> GetCalendarItemsWithSubCulendar(DateTime? dt, DateTime? dt2, int depth = 3)
        {
            
            List<Models.CalendarItem> calItems = new List<Models.CalendarItem>();
            var firstParent = _dbContext.Calendars.Where(c => c.Paent == null).Select(x => x.Id).FirstOrDefault();
            var test = GetReccursiveItems(dt, dt2, firstParent, calItems);
            var filteredParameters = test.Select(x => CalendarItemsMapper.FilterData(x)).ToList();
            return filteredParameters;
        }
        public List<CalendarItem> FillCalendarItems(Models.Calendar calendar)
        {
            List<CalendarItem> items = new List<CalendarItem>();
            DateTime start = calendar.StartDate.Value;
            DateTime end = calendar.EndDate.Value;
            DateTime tempDate = start;
            var rangeDates = (end - start).Days;
            Models.CalendarItem item;
            var holidays = _dbContext.Holidays.Select(x => new { x.DateDay, x.DateMonth });
            var WeekDays = _dbContext.WorkingDays.Select(x=> new {x.Name, x.IsWorkingDay});
            var dict = new Dictionary<string, bool>();
            
            var weekend = false;
            for (int i = 0; i <= rangeDates; i++)
            {
                var currentDay = tempDate.ToString("dddd");
                var workingDay = WeekDays.Where(x => x.Name == currentDay).Select(x=>x.IsWorkingDay).FirstOrDefault();
                if(currentDay.Equals("Sunday") || currentDay.Equals("Saturday"))
                {
                    weekend = true;
                }
                var holiday = holidays.Any(x => (x.DateDay).Equals(tempDate.Day.ToString()) && x.DateMonth.Equals(tempDate.Month.ToString()));
                if (holiday)
                {
                    workingDay = false;
                }
                item = new Models.CalendarItem(null, calendar, calendar.Description, tempDate, holiday, weekend, workingDay, false, true, calendar.IsApproved);
                var dbItem = FullCalendarMapper.ToDatabase(item);
                items.Add(dbItem);
                tempDate = tempDate.AddDays(1);

            }
            return items;
        }
        public Models.Calendar AddCalendar(Models.Calendar calendar)
        {
            var items = FillCalendarItems(calendar);
            calendar.CreatedDate = DateTime.Now;
            var alreadyExists = _dbContext.Calendars.Where(x => x.Description == calendar.Description).FirstOrDefault();
            if (alreadyExists==null)
            {
                var dbCalendar = CalendarMapper.ToDatabase(calendar);
                _dbContext.Calendars.Add(dbCalendar);
            }
            var datesExisting = _dbContext.CalendarItems.Where(x => x.CalendarId == calendar.Id).Select(x=>x.Date).ToList();
            var filtered = items.Where(x => !datesExisting.Contains(x.Date));
            _dbContext.CalendarItems.AddRange(filtered);
            _dbContext.SaveChanges();
            return calendar;

        }

        //For whole calendar
        public void Delete(Guid id)
        {
            var calendar = _dbContext.Calendars.AsNoTracking().FirstOrDefault(cal => cal.Id == id);
            var calendarItems = _dbContext.CalendarItems.AsNoTracking().Where(cal => cal.CalendarId == id).ToList();

            var children = _dbContext.Calendars.AsNoTracking().Where(cal => cal.PaentId == id).ToList();
            foreach (var child in children)
            {
                var nove = _dbContext.CalendarItems.AsNoTracking().Where(cal => cal.CalendarId == child.Id).ToList();
                var schedulerItems = _dbContext.SchedulerItems.AsNoTracking().Where(x=>nove.Contains(x.CalendarItems)).ToList();
                _dbContext.SchedulerItems.RemoveRange(schedulerItems);
                _dbContext.CalendarItems.RemoveRange(nove);
                _dbContext.SaveChanges();
                Delete(child.Id);

            }
            _dbContext.CalendarItems.RemoveRange(calendarItems);
            _dbContext.Calendars.Remove(calendar);
            _dbContext.SaveChanges();

        }

        //For whole calendar
        public void Edit(Models.Calendar calendar)
        {
            calendar.CreatedDate = DateTime.Now;
            var dbCalendar = CalendarMapper.ToDatabase(calendar);
            var x = _dbContext.Calendars.AsNoTracking().FirstOrDefault(cal => cal.Id == dbCalendar.Id);
            dbCalendar.RowVersion = x.RowVersion;
            var calendarItems = _dbContext.CalendarItems.Where(cal => cal.CalendarId == calendar.Id);

            _dbContext.CalendarItems.RemoveRange(calendarItems);
            var items = FillCalendarItems(calendar);
            _dbContext.CalendarItems.AddRange(items);
            _dbContext.Calendars.Update(dbCalendar);
            _dbContext.SaveChanges();

        }

        //For 1 particular day from calendar
        public void EditCalendarItem(Models.CalendarItem calendarItem)
        {
            var dbCIItem = CalendarItemsMapper.ToDatabase(calendarItem);
            _dbContext.Update(dbCIItem);
            _dbContext.SaveChanges();

        }

        public Models.CalendarItem AddCalendarItem(Guid calendarId,Models.CalendarItem calendarItem)
        {
            
            var dbCalendarItem = CalendarItemsMapper.ToDatabase(calendarItem);
            dbCalendarItem.CalendarId = calendarId;
            _dbContext.CalendarItems.Add(dbCalendarItem);
            _dbContext.SaveChanges();
            var result = _dbContext.CalendarItems.SingleOrDefault(X=>X.Date== calendarItem.Date && X.GivenName==calendarItem.GivenName);
            return CalendarItemsMapper.FromDbCalendarItems(result);
        }

        public void DeleteCalendarItem(Guid id)
        {
            var calendarItem = _dbContext.CalendarItems.Where(x => x.Id == id).FirstOrDefault();
            var schedulerItems = _dbContext.SchedulerItems.Where(x => x.CalendarItemsId==id).ToList();
            _dbContext.RemoveRange(schedulerItems);
            _dbContext.CalendarItems.Remove(calendarItem);
            _dbContext.SaveChanges();
        }
    }
}
