using Domain.Model;
using Domain.Model.SubItemMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;

namespace Application.Services.Master
{
    public partial class SubItemWarehouseMappingService : ISubItemWarehouseMappingService
    {
        #region Fields
        private readonly IRepository<SubItemWareHouseMapping> _subitemMappingRepository;
        private readonly IRepository<SubItemDb> _subItemRepository;
        #endregion
        #region Ctor
        public SubItemWarehouseMappingService(IRepository<SubItemWareHouseMapping> subitemMappingRepository, IRepository<SubItemDb> subItemRepository)
        {
            _subitemMappingRepository = subitemMappingRepository;
            _subItemRepository = subItemRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(SubItemWareHouseMapping entity)
        {
            _subitemMappingRepository.Insert(entity);
        }

        public virtual void Update(SubItemWareHouseMapping entity)
        {
            _subitemMappingRepository.Update(entity);
        }

        public virtual SubItemWareHouseMapping GetById(int id)
        {
            return _subitemMappingRepository.GetById(id);
        }

        public virtual IPagedList<SubItemWareHouseMapping> GetAllSubItemMapping(string subitemName = "", int PageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _subitemMappingRepository.Table
                        select x;
            if (!string.IsNullOrEmpty(subitemName))
            {
                query = from x in query
                        join y in _subItemRepository.Table
                            on x.SubItemCode equals y.SubItemCode
                        where y.SubItemName.Contains(subitemName)
                        select x;
            }

            query = query.OrderByDescending(x => x.Id);
            return new PagedList<SubItemWareHouseMapping>(query, PageIndex, pageSize);
        }


        public virtual SubItemWareHouseMapping GetItemLocation(string subItemCode)
        {
            var query = from x in _subitemMappingRepository.Table
                        select x;
            query = query.Where(x => x.SubItemCode == subItemCode);
            return query.FirstOrDefault();
        }
        #endregion
    }
}
