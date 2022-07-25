using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;

namespace Application.Services.PO
{
    public partial class StockTransferPo : IStockTransferPo
    {
        #region Fields
        private readonly IRepository<StockTransferPoDb> _stockTransfterPoRepository;

        #endregion

        #region Ctor
        public StockTransferPo(IRepository<StockTransferPoDb> StockTransferPo)
        {
            _stockTransfterPoRepository = StockTransferPo;
        }
        public void Insert(StockTransferPoDb stockTransferPo)
        {
            _stockTransfterPoRepository.Insert(stockTransferPo);
        }

        public void Update(StockTransferPoDb stockTransferPo)
        {
            _stockTransfterPoRepository.Update(stockTransferPo);
        }

        public virtual IPagedList<StockTransferPoDb> GetStockTransferPos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _stockTransfterPoRepository.Table
                        select x;
            if (poNumber != "0")
            {
                query = query.Where(x => x.PONumber == poNumber);
            }
            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<StockTransferPoDb>(query, pageIndex, pageSize);
            return result;
        }
        #endregion
    }
}
