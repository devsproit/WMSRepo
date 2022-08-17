using Domain.Model.Masters;
using WMS.Core;

namespace Application.Services.Master
{
    public interface IVenderVehicleService
    {
        void Insert(VendorVehicle entity);
        IPagedList<VendorVehicle> VendorVehicles(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}