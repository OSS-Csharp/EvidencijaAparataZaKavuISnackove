using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models.DetailedModels;

namespace VendingMachine.Domain.Models.DetaildModels
{
    public class DetailedItem
    {
        public int _id { get; set; }
        public string _itemName { get; set; }
        public float _price { get; set; }
        public virtual List<DetailedMachineItem> _detailedMachineItems { get; set; }
        private static List<DetailedMachineItem> GetManyToMany(ItemEntity entity)
        {
            var lst = new List<DetailedMachineItem>();
            foreach (var ents in entity.vendingMachineItemEntities)
            {
                if(ents.item != null)
                {
                    lst.Add(new DetailedMachineItem
                    {
                        _vendingMachineId = ents.vendingMachineId,
                        _vendingMachineValidity = ents.vendingMachine.machineValidity,
                        _itemId = ents.itemId,
                        _itemName = ents.item.itemName,
                        _itemPrice = ents.item.price,
                        _amountOfAnItem = ents.amountOfItem
                    });
                }
            }
            return lst;
        }
        public static DetailedItem MapFromDetailed(ItemEntity itemEntity)
        {
            return new DetailedItem
            {
                _id = itemEntity.id,
                _itemName = itemEntity.itemName,
                _price = itemEntity.price,
                _detailedMachineItems = GetManyToMany(itemEntity)
            };
        }
    }
}
