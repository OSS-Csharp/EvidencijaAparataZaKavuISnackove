using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain.Entity;

namespace VendingMachine.Domain.Models.DetailedModels
{
    public class DetailedMachine
    {
        public int _id { get; set; }
        public bool _machineValidity { get; set; }
        public virtual List<DetailedMachineItem> _vendingMachineItemEntities { get; set; }
        private static List<DetailedMachineItem> GetManyToMany(VendingMachineEntity entity)
        {
            var lst = new List<DetailedMachineItem>();

            if(entity.vendingMachineItemEntities != null) 
            {
                foreach (var ents in entity.vendingMachineItemEntities)
                {
                    if (ents.item != null)
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
            return null;
        }
        public static DetailedMachine MapFromDetailed(VendingMachineEntity vendingMachineEntity)
        {
            return new DetailedMachine
            {
                _id = vendingMachineEntity.id,
                _machineValidity = vendingMachineEntity.machineValidity,
                _vendingMachineItemEntities = GetManyToMany(vendingMachineEntity)
            };
        }
    }
}
