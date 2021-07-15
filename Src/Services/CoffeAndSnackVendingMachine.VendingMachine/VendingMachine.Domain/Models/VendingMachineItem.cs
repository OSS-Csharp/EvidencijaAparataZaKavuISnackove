using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain.Models
{
    public class VendingMachineItem
    {
        public int _vendingMachineId { get; set; }
        public int _itemId { get; set; }
        public int _amountOfAnItem { get; set; }
        
        public static VendingMachineItem MapFrom(Entity.VendingMachineItemEntity vendingMachineItem)
        {
            return new VendingMachineItem
            {
                _vendingMachineId = vendingMachineItem.vendingMachineId,
                _itemId = vendingMachineItem.itemId,
                _amountOfAnItem = vendingMachineItem.amountOfItem
            };
        }
    }
}
