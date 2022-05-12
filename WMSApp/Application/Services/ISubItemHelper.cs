using Domain.Model;

namespace Application.Services;

public interface ISubItemHelper
{
    bool DeleteSubItemById(int Id);
    List<SubItemDb> GetAllSubItem();
    SubItemDb GetSubItemById(int Id);
    bool UpdateSubItemById(SubItemDb SubItem);
    bool CreateNewSubItem(SubItemDb SubItem);
}