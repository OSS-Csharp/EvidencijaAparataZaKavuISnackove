using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public interface IBaseRepository
    {
        public IEnumerable<ApplicationUser> GetAllUsers();
        public ApplicationUser GetUserByUserName(string userName);
        public void AssignJob(string userName, int aparatId);
        public Schedule GetUserSchedule(string userName);
        public IEnumerable<ApplicationUser> GetAvailables();
    }
}
