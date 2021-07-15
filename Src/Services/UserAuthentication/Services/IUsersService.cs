using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
    }
}
