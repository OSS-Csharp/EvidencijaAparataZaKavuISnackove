using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public interface IScheduleService
    {
        Task<Schedule> GetUserSchedule(string userName);
    }
}
