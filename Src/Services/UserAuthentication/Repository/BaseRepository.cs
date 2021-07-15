using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Data;
using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
        public Schedule GetUserSchedule(string userName)
        {
            return _dbContext.Schedules.FirstOrDefault(i => i.UserName == userName);
        }
        public ApplicationUser GetUserByUserName(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(i => i.UserName == userName);
            if (user != null)
                return user;
            return null;
        }
        public void AssignJob(string userName, int aparatId)
        {
            var user = _dbContext.Users.Single(i => i.UserName == userName);
            user.Availability = false;
            Schedule schedule = new Schedule()
            {
                UserName = userName,
                AparatId = aparatId
            };
            _dbContext.Update(user);
            _dbContext.Schedules.Add(schedule);
            _dbContext.SaveChanges();
        }
        public IEnumerable<ApplicationUser> GetAvailables()
        {
            var users = GetAllUsers();
            List<ApplicationUser> avUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (user.Availability == true)
                {
                    avUsers.Add(user);
                }
            }
            return avUsers;
        }
    }
}
