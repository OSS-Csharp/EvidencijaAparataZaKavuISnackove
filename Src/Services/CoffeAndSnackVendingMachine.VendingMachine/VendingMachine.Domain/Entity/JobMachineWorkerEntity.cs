using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain.Entity
{
    public class JobMachineWorkerEntity
    {
        public int id { get; set; }
        public int vendingMachineId { get; set; }
        public VendingMachineEntity vendingMachine { get; set; }
        public int workerId { get; set; }

        public int intworkerIdOriginal { get; set; }
        public WorkerEntity worker { get; set; } 
        public bool finished { get; set; }
        
    }
}
