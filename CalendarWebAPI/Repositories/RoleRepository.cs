using CalendarWebAPI.DbModels;

namespace CalendarWebAPI.Repositories
{
    public class RoleRepository
    {
        private readonly CalendarContext _calendarContext;

        public RoleRepository(CalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
        }

        public IEnumerable<Models.Role> GetRoles()
        {
            return _calendarContext.Roles.Select(x=>new Models.Role(x.Id,x.Name)).ToList();
        }
    }
}
