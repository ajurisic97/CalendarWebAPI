using CalendarWebAPI.DbModels;

namespace CalendarWebAPI.Repositories
{
    public class WorkingDayRepository
    {
        private readonly CalendarContext _calendarContext;
        public WorkingDayRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public IEnumerable<WorkingDay> GetWorkingDays()
        {
            return _calendarContext.WorkingDays.ToList();
        }
        public void UpdateWorkingDays(WorkingDay wd)
        {
            _calendarContext.WorkingDays.Update(wd);
            _calendarContext.SaveChanges();
        }
        
    }
}
