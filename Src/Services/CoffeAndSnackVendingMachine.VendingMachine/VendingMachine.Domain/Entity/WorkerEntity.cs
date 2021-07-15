using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain.Entity
{
    public class WorkerEntity 
    {
        public int id { get; set; }
        public int workerIdOriginal { get; set; }
        public string name { get; set; }
        public ICollection<JobMachineWorkerEntity> jobMachineWorkerEntity { get; set; }

    }
}
