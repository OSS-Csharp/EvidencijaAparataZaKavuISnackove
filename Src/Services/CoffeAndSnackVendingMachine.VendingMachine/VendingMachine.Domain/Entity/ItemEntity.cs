using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendingMachine.Domain.Entity
{
    public class ItemEntity
    {
        [Key]
        public int id { get; set; }
        public string itemName { get; set; }
        public float price { get; set; }
        public UnitQuantity unit { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.InverseProperty("item")]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<VendingMachineItemEntity> vendingMachineItemEntities { get; set; }

    }
}
