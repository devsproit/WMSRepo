using Domain.Model;
using System.Collections.Generic;
using System.Linq;
namespace Application.Services
{

    public interface IItemHelper
    {
        bool DeleteItemById(int Id);
        List<ItemDb> GetAllItem();
        ItemDb GetItemById(int Id);
        bool UpdateItemById(ItemDb Item);
        bool CreateNewItem(ItemDb Item);
    }
}