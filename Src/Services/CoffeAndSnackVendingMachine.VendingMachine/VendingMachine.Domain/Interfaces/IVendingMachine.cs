using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain.Models;

namespace VendingMachine.Domain.Interfaces
{
    public interface IVendingMachine
    {
        bool SetVendingMachineObjectValiditiBool(bool value, int id);
        Item GetAllAvailableItemsInVendingMachineObject(int vendingMachineID);
        int GetItemAmountFromVendingMachineObject(int vendingMachineID, int itemID);
        IEnumerable<VendingMachineObject> GetAllVendingMachineObjecs();
        /**/IEnumerable<Item> GetAllItems();  
        VendingMachineObject GetVendingMachineObjectByID(int id);
        VendingMachineObject SetItemInVendigMachineObject(int vendingMachineID, string itemName);
        Item SetNewItem();
        VendingMachineObject CreateNewVendingMachine();
    }
}
