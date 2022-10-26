using Microsoft.AspNetCore.Http;
using CalendarWebAPI.DbModels;
using CalendarWebAPI.Helper;
using CalendarWebAPI.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace CalendarWebAPI.Repositories
{
    public class UserRepository
    {
        private readonly CalendarContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserRepository(CalendarContext dbContext, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public List<Models.User> GetAll()
        {
            var users = _dbContext.Users.Include(x=>x.UserRoles).ThenInclude(x=>x.Role).Select(x => UserMapper.FromDatabase(x)).ToList();
            return users;
        }
        public Models.User GetUser(string username)
        {
            //var hashing = new HashingManager();
            var dbUser = _dbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Include(x => x.Person).AsEnumerable().Where(u => u.Username.ToLower() == username.ToLower()).FirstOrDefault();
            var us = UserMapper.FromDatabase(dbUser);
            //var dbUser = _dbContext.Users.Where(u => u.Username.ToLower() == user.UserName.ToLower() && u.Password == user.Password).Include(x => x.UserRoles).ThenInclude(x => x.Role).Include(x => x.Person).FirstOrDefault();
            //var us = UserMapper.FromDatabase(dbUser);
            return us;
        }

        public Models.User GetById(Guid guid)
        {
            var dbUser = _dbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Include(x => x.Person).AsEnumerable().Where(u => u.Id == guid).FirstOrDefault();
            var us = UserMapper.FromDatabase(dbUser);

            return us;
        }
        public Models.User Add(Models.User user)
        {
            var hashing = new HashingManager();
            var hash = hashing.HashToString(user.Password);
            user.Password = hash;
            // ovdje je toString mozda visak
            //CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            //user.Password = passwordHash.ToString();
            var dbUser = UserMapper.ToDatabase(user);

            if (!_dbContext.Users.Any(x=>x.Username.Equals(user.UserName) || x.Email.Equals(user.Email)))
            {
                _dbContext.Users.Add(dbUser);
                _dbContext.SaveChanges();
            }
            else
            {
                return null;
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
                    UserId = user.Id,
                    RoleId = role.Id,
                };
                
                userRoles.Add(dbUR);
                
                dbUser.UserRoles = userRoles;
                if(!_dbContext.UserRoles.Any(x=>x.RoleId == dbUR.RoleId && x.UserId == user.Id))
                _dbContext.UserRoles.Add(dbUR);

            }

            _dbContext.Users.Update(dbUser);
            _dbContext.Entry(dbUser).Property(x => x.Password).IsModified = false;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid guid)
        {
            var user = _dbContext.Users.Where(x => x.Id == guid).FirstOrDefault();
            if (user != null)
            {
                _dbContext.UserRoles.RemoveRange(_dbContext.UserRoles.Where(x => x.UserId == guid).ToList());
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        private Models.User Authenticate(Models.User user)
        {
            HashingManager hashing = new HashingManager();
            var currentUser = _dbContext.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Include(x => x.Person).AsEnumerable().Where(u => u.Username.ToLower() == user.UserName.ToLower() && hashing.Verify(user.Password, u.Password)).FirstOrDefault();
            if (currentUser != null){
                return UserMapper.FromDatabase(currentUser);
            }
            return null;
        }


        public string Login(string username, string password)
        {
            var userLogin = new Models.User(username, password);
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                _contextAccessor.HttpContext.Request.Headers.Add("Authorization","Bearer "+token);
                return "bearer "+ token;
            }

            return "User not found";
        }

        public void Signout()
        {
            _contextAccessor.HttpContext.Request.Headers.Remove("Authorization");
        }

        private string Generate(Models.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = user.Role == null ? "None" : user.Role;
            var person = user.PersonId == null ? Guid.Empty.ToString() : user.PersonId.ToString();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.UserData, person),
                new Claim("id",person)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
