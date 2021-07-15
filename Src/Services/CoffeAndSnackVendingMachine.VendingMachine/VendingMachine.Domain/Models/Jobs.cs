using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Domain.Models
{
    public class Jobs 
    {                 
        public int _id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public int _vendingMachineId{ get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public int _workerId{ get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public bool _finished { get; set; }

        public static Jobs MapFrom(Entity.JobMachineWorkerEntity job) 
        {
            return new Jobs
            {
                _id = job.id,
                _vendingMachineId=job.vendingMachineId,
                _workerId=job.workerId,
                _finished = job.finished
                
            };
        }
    }
}
