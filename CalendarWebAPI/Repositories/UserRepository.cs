
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
        public Models.User Add(Models.User user)
        {
            var dbUser = UserMapper.ToDatabase(user);
            var role = _dbContext.Roles.Where(x => x.Name == user.Role).First();
            //var userRole = _dbContext.UserRoles.Where(x => x.Role == role);
            //dbUser.UserRoles = userRole.ToList();
            if (!_dbContext.Users.Any(x=>x.Username.Equals(user.UserName) || x.Email.Equals(user.Email)))
            {
                _dbContext.Users.Add(dbUser);
                _dbContext.SaveChanges();
            }
            var result = _dbContext.Users.SingleOrDefault(x => x.Username == user.UserName);
            return UserMapper.FromDatabase(result);
        }
        public void Edit(Models.User user)
        {
            //var current = _dbContext.Users.Where(x=>x.Id==user.Id).FirstOrDefault();

            var role = _dbContext.Roles.Where(x => x.Name == user.Role).First();
            
            var dbUser = UserMapper.ToDatabase(user);
            //dbUser.Password = current.Password;
            var dbUR = new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = dbUser.Id,
                RoleId = role.Id,
            };
            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(dbUR);
            dbUser.UserRoles = userRoles;
            
            _dbContext.Users.Update(dbUser);
            _dbContext.UserRoles.Add(dbUR);
            _dbContext.SaveChanges();
        }
    }
}
