﻿using CalendarWebAPI.DbModels;
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

        public List<FullCalendarDto> GetCalendarsWithSubCalendars(DateTime? dt, DateTime? dt2, int depth=7)
        {
            var result = _dbContext.Calendars.Where(c => c.Paent == null).Select(GetCalendarProjection(dt, dt2, depth, 0));
            return result.ToList();
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
            var WeekDays = _dbContext.WorkingDays.Select(x => new Models.WorkingDays( x.Monday, x.Tuseday, x.Wednesday, x.Thursday, x.Friday, x.Sathurday, x.Sunday)).FirstOrDefault();
            var dict = new Dictionary<string, bool>();
            dict.Add("Monday", WeekDays.Monday); dict.Add("Tuesday", WeekDays.Tuesday); dict.Add("Wednesday", WeekDays.Wednesday); dict.Add("Thursday", WeekDays.Thursday); 
            dict.Add("Friday", WeekDays.Friday); dict.Add("Saturday", WeekDays.Saturday); dict.Add("Sunday", WeekDays.Sunday);

            var workingDay = true;
            var weekend = false;
            for (int i = 0; i <= rangeDates; i++)
            {
                var currentDay = tempDate.ToString("dddd");
                workingDay = dict[currentDay];
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


    }
}
