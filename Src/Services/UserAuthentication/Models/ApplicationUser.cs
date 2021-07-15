using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAuthentication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Boolean Availability { get; set; }
        public static ApplicationUser MapFrom(Entity.UserEntity user)
        {
            return new ApplicationUser
            {
                Id = user.id,
                UserName = user.UserName,
                Email = user.Email,
                Availability = user.Availability,

            };
        }
    }
}
