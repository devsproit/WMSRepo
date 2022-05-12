using Domain.Model;
using System.Collections.Generic;
using System.Linq;
namespace Application.Services

{


    public interface ISubItemHelper
    {
        bool DeleteSubItemById(int Id);
        List<SubItemDb> GetAllSubItem();
        SubItemDb GetSubItemById(int Id);
        bool UpdateSubItemById(SubItemDb SubItem);
        bool CreateNewSubItem(SubItemDb SubItem);
        SubItemDb GetSubItemByItemId(int id);

    }
}