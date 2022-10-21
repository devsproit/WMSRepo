using Domain.Model.SubItemMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Application.Services.Master
{
    public partial interface ISubItemWarehouseMappingService
    {
        void Insert(SubItemWareHouseMapping entity);
        void Update(SubItemWareHouseMapping entity);
        SubItemWareHouseMapping GetById(int id);
        IPagedList<SubItemWareHouseMapping> GetAllSubItemMapping(string subitemName = "", int PageIndex = 0, int pageSize = int.MaxValue);
    }
}
