using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using WMS.Core;
namespace Application.Services.PO
{
    public partial class SalePo : ISalePo
    {
        #region Fields
        private readonly IRepository<SalePoDb> _salePoRepository;

        #endregion

        #region Ctor
        public SalePo(IRepository<SalePoDb> salePoRepository)
        {
            _salePoRepository = salePoRepository;
        }
        public void Insert(SalePoDb salePoDb)
        {
            _salePoRepository.Insert(salePoDb);
        }

        public void Update(SalePoDb salePoDb)
        {
            _salePoRepository.Update(salePoDb);
        }
        public virtual SalePoDb GetById(int id)
        {
            return _salePoRepository.GetById(id);
        }
        public virtual IPagedList<SalePoDb> GetSalePos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _salePoRepository.Table
                        select x;

            if (poNumber != "0")
            {
                query = query.Where(x => x.PONumber == poNumber);
            }

            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<SalePoDb>(query, pageIndex, pageSize);
            return result;
        }
        #endregion
    }
}
