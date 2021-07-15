using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAuthentication.Entity
{
    public class UserEntity
    {
        [Key]
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Availability { get; set; }
    }
}
