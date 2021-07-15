using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendingMachine.Domain.Entity
{
    public class VendingMachineEntity
    {
        [Key]
        public int id { get; set; }
        public bool machineValidity { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.InverseProperty("vendingMachine")]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<VendingMachineItemEntity> vendingMachineItemEntities { get; set; }
    }
}
