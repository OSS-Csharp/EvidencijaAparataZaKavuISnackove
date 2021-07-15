using System;
using System.Collections.Generic;
using System.Text;

public enum UnitQuantity
{
    mililitres,
    grams,
    peaces
}

namespace VendingMachine.Domain.Models
{
    public class Item
    {
        public int _id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string _itemName { get; set; }
        public float _price { get; set; }
        
        public static Item MapFrom(Entity.ItemEntity item)
        {
            return new Item
            {
                _id = item.id,
                _itemName = item.itemName,
                _price = item.price
            };
        }
    }
}
