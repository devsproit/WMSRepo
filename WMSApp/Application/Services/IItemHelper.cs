using Domain.Model;

namespace Application.Services;

public interface IItemHelper
{
    bool DeleteItemById(int Id);
    List<ItemDb> GetAllItem();
    ItemDb GetItemById(int Id);
    bool UpdateItemById(ItemDb Item);
    bool CreateNewItem(ItemDb Item);
}