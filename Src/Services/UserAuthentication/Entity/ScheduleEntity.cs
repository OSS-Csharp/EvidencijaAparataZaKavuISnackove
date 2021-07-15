using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAuthentication.Entity
{
    public class ScheduleEntity
    {
        [Key]
        public int id { get; set; }
        public string userName { get; set; }
        public int AparatId { get; set; }

    }
}
