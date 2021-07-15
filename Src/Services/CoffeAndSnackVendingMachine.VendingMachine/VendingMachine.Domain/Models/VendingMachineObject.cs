using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain.Interfaces;

namespace VendingMachine.Domain.Models
{
    public class VendingMachineObject
    {
        public int _id { get; set; }
        public bool _machineValidity { get; set; }

        public static VendingMachineObject MapFrom(Entity.VendingMachineEntity entity)
        {
            return new VendingMachineObject
            {
                _id = entity.id,
                _machineValidity = entity.machineValidity,
            };
        }
    }
}
