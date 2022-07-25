using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;
using Domain.Model.Invoice;
using WMS.Core;
namespace Application.Services.Invoice
{
    public partial class InvoiceService : IInvoiceService
    {

        #region Fields
        private readonly IRepository<InvoiceMaster> _invoiceMasterRepository;
        private readonly IRepository<InvoiceDetails> _invocieDetailsRepository;
        #endregion

        #region Ctor
        public InvoiceService(IRepository<InvoiceMaster> invoiceMasterRepository, IRepository<InvoiceDetails> invocieDetailsRepository)
        {
            _invoiceMasterRepository = invoiceMasterRepository;
            _invocieDetailsRepository = invocieDetailsRepository;
        }
        #endregion

        #region Methods
        public virtual int InsertMaster(InvoiceMaster entity)
        {
            _invoiceMasterRepository.Insert(entity);
            return entity.Id;   
        }


        public virtual void InsertDetails(InvoiceDetails entity)
        {
            _invocieDetailsRepository.Insert(entity);
        }


        public virtual void InsertDetails(List<InvoiceDetails> entities)
        {
            _invocieDetailsRepository.Insert(entities);
        }


        public virtual IPagedList<InvoiceMaster> GetAllMaster(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _invoiceMasterRepository.Table
                        select x;
            var result = new PagedList<InvoiceMaster>(query, pageIndex, pageSize);
            return result;
        }
        #endregion
    }
}
