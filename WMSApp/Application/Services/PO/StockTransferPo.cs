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
        private readonly IRepository<PurchaseOrderDb> _poRepository;

        #endregion

        #region Ctor
        public StockTransferPo(IRepository<StockTransferPoDb> StockTransferPo, IRepository<PurchaseOrderDb> poRepository)
        {
            _stockTransfterPoRepository = StockTransferPo;
            _poRepository = poRepository;
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

        public virtual IPagedList<StockTransferPoDb> GetDetails(string PoCat, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _poRepository.Table
                        join y in _stockTransfterPoRepository.Table
                        on x.PONumber equals y.PONumber
                        where x.POCategory == PoCat
                        select y;
            var result = new PagedList<StockTransferPoDb>(query, pageIndex, pageSize);
            return result;
        }

        public StockTransferPoDb GetById(int id)
        {
            return _stockTransfterPoRepository.GetById(id);
        }

        public virtual IPagedList<StockTransferPoDb> GetAllStockTrasnferPo(string status, string branchCode, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _poRepository.Table
                        join y in _stockTransfterPoRepository.Table
                        on x.PONumber equals y.PONumber
                        where x.POCategory == "StockTransfer PO" && x.BranchCode == branchCode
                        select y;
            if (status == "ALL")
            {

            }
            else if (status == "Done")
            {
                query = query.Where(x => x.IsProcessed == true);
            }
            else
            {
                query = query.Where(x => x.IsProcessed == false);
            }

            query = query.OrderByDescending(x => x.Id);

            return new PagedList<StockTransferPoDb>(query, pageIndex, pageSize);
        }
        #endregion
    }
}
