
using CalendarWebAPI.DbModels;
using CalendarWebAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebAPI.Repositories
{
    public class UserRepository
    {
        private readonly CalendarContext _dbContext;
        public UserRepository(CalendarContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Models.User> GetAll()
        {
            var users = _dbContext.Users.Include(x=>x.UserRoles).ThenInclude(x=>x.Role).Select(x => UserMapper.FromDatabase(x)).ToList();
            return users;
        }
        public Models.User GetUser(Models.User user)
        {
            var dbUser = _dbContext.Users.Where(u => u.Username.ToLower() == user.UserName.ToLower() && u.Password == user.Password).Include(x=>x.UserRoles).ThenInclude(x=>x.Role).Include(x=>x.Person).FirstOrDefault();
            var us = UserMapper.FromDatabase(dbUser);
            return us;
        }
    }
}
