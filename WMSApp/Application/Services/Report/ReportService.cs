using Domain.Model.Invoice;
using Domain.Model.PO;
using Domain.Model.PS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

namespace Application.Services.Report
{
    public partial class ReportService:IReportService
    {


        #region Fields
        private readonly IRepository<SalePoDb> _salePoRepository;
        private readonly IRepository<ServiceOrderPODb> _serviceOrderPoRepository;
        private readonly IRepository<StockTransferPoDb> _stockTransfterPoRepository;
        private readonly IRepository<InvoiceMaster> _invoiceMasterRepository;
        private readonly IRepository<PickSlipMaster> _pickSlipMasterRepository;
        #endregion

        #region Ctor
        public ReportService(IRepository<SalePoDb> salePoRepository, IRepository<ServiceOrderPODb> serviceOrderPoRepository, IRepository<StockTransferPoDb> stockTransfterPoRepository, IRepository<InvoiceMaster> invoiceMasterRepository, IRepository<PickSlipMaster> pickSlipMasterRepository)
        {
            _salePoRepository = salePoRepository;
            _serviceOrderPoRepository = serviceOrderPoRepository;
            _stockTransfterPoRepository = stockTransfterPoRepository;
            _invoiceMasterRepository = invoiceMasterRepository;
            _pickSlipMasterRepository = pickSlipMasterRepository;
        }
        #endregion

        #region Methods
        public int PendingPickUp()
        {
            var pendingSalePo = from x in _salePoRepository.Table
                                where x.IsProcessed == false
                                select x;
            var pendingServicePo = from x in _serviceOrderPoRepository.Table
                                   where x.IsProcessed == false
                                   select x;
            var pendingStockTransferPo = from x in _stockTransfterPoRepository.Table
                                         where x.IsProcessed == false
                                         select x;
            int total = pendingSalePo.Count() + pendingServicePo.Count() + pendingStockTransferPo.Count();
            return total;
        }

        public int PendingInvoice()
        {
            var query = from x in _pickSlipMasterRepository.Table
                        where x.IsProcessed == false
                        select x;
            return query.Count();
        }

        public int PendingDispatch()
        {
            var query = from x in _invoiceMasterRepository.Table
                        where x.DispatchDone == false
                        select x;
            return query.Count();
        }
        #endregion

    }
}
