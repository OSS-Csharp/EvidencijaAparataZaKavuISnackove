using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Models;
using VendingMachine.Domain.Models.DetaildModels;

namespace VendingMachine.UserInterface.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<DetailedItem> GetItemById(int id);
        Task<Item> CreateNewItem(Item entity);
        Task<Item> UpdadeItem(Item updatedItem);
        Task DeleteItem(int id);
    }
}
