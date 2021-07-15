using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models.DetaildModels;

namespace VendingMachine.Domain.Models.DetailedModels
{
    public class DetailedMachineItem
    {
        public int _vendingMachineId { get; set; }
        public bool _vendingMachineValidity { get; set; }
        public int _itemId { get; set; }
        public string _itemName { get; set; }
        public float _itemPrice { get; set; }
        public int _amountOfAnItem { get; set; }
        public static DetailedMachineItem MapFromDetailed(VendingMachineItemEntity vendingMachineItemEntity)
        {
            return new DetailedMachineItem
            {
                _vendingMachineId = vendingMachineItemEntity.id,
                _vendingMachineValidity = vendingMachineItemEntity.vendingMachine.machineValidity,
                _itemId = vendingMachineItemEntity.itemId,
                _itemName = vendingMachineItemEntity.item.itemName,
                _itemPrice = vendingMachineItemEntity.item.price,
                _amountOfAnItem = vendingMachineItemEntity.amountOfItem
            };
        }
    }
}
