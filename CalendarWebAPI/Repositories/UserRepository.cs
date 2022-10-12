
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
            //var role = _dbContext.Roles.Where(x => x.Id == user.RoleId).First();
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
            
            var dbUser = UserMapper.ToDatabase(user);
            //dbUser.Password = current.Password;
            List<UserRole> userRoles = new List<UserRole>();
            _dbContext.UserRoles.RemoveRange(_dbContext.UserRoles.Where(x => x.UserId == dbUser.Id && x.RoleId !=user.RoleId).ToList());
            if (user.RoleId != Guid.Empty)
            {
                var role = _dbContext.Roles.Where(x => x.Id == user.RoleId).First();
                var dbUR = new UserRole()
                {
                    Id = Guid.NewGuid(),
                    UserId = dbUser.Id,
                    RoleId = role.Id,
                };
                
                userRoles.Add(dbUR);
                
                dbUser.UserRoles = userRoles;
                if(!_dbContext.UserRoles.Any(x=>x.RoleId == dbUR.RoleId && x.UserId == dbUR.UserId))
                _dbContext.UserRoles.Add(dbUR);

            }

            _dbContext.Users.Update(dbUser);
            _dbContext.Entry(dbUser).Property(x => x.Password).IsModified = false;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid guid)
        {
            _dbContext.UserRoles.RemoveRange(_dbContext.UserRoles.Where(x => x.UserId == guid).ToList());
            _dbContext.Users.Remove(_dbContext.Users.Where(x => x.Id == guid).FirstOrDefault());
            _dbContext.SaveChanges();
        }
    }
}
