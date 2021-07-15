using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendingMachine.Domain.Entity
{
    //[Table("VendingMachineItemEntity")]
    public class VendingMachineItemEntity
    {
        [Key]
        public int id { get; set; }

        public int vendingMachineId { get; set; }
        [ForeignKey("vendingMachineId")]
        [InverseProperty("vendingMachineItemEntities")]
        public virtual VendingMachineEntity vendingMachine { get; set; }
        public int itemId { get; set; }
        [ForeignKey("itemId")]
        [InverseProperty("vendingMachineItemEntities")]
        public virtual ItemEntity item { get; set; }
        public int amountOfItem { get; set; }
    }
}
