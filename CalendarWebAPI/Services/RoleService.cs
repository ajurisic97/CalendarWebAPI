using CalendarWebAPI.Repositories;

namespace CalendarWebAPI.Services
{
    public class RoleService
    {
        public RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public IEnumerable<Models.Role> GetAll()
        {
            return _roleRepository.GetRoles();
        }
    }
}
