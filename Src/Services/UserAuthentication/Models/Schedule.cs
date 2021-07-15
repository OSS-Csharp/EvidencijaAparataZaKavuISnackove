using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAuthentication.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public string UserName { get; set; }
        public int AparatId { get; set; }
        public static Schedule MapFrom(Entity.ScheduleEntity schedule)
        {
            return new Schedule
            {
                ScheduleId = schedule.id,
                UserName = schedule.userName,
                AparatId = schedule.AparatId
            };
        }
    }
}
