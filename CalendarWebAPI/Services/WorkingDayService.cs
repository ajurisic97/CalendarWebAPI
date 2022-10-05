using CalendarWebAPI.DbModels;
using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class WorkingDayService
    {
        public WorkingDayRepository _workingDayRepository;
        public WorkingDayService(WorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
        }

        public IEnumerable<WorkingDay> GetWorkingDays()
        {
            return _workingDayRepository.GetWorkingDays();
        }
        public void UpdateWorkingDays(WorkingDay wd)
        {
            _workingDayRepository.UpdateWorkingDays(wd);
        }
    }
}
