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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchType">ALL,DP,NDP</param>
        /// <returns></returns>
        public virtual IPagedList<InvoiceMaster> GetAllMaster(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue,string searchType="ALL")
        {
            var query = from x in _invoiceMasterRepository.Table
                        select x;
            query = query.Where(x => x.BranchCode == branchCode);
            if (searchType=="DP")
            {
                query = query.Where(x => x.DispatchDone == true);

            }
            else if(searchType=="NDP")
            {
                query = query.Where(x => x.DispatchDone == false);
            }
            else
            {
                
            }
            query = query.OrderByDescending(x => x.Id);

            var result = new PagedList<InvoiceMaster>(query, pageIndex, pageSize);

            return result;
        }

        public virtual InvoiceMaster GetById(int id)
        {
            return _invoiceMasterRepository.GetById(id);
        }
        public virtual void Update(InvoiceMaster entity)
        {
            _invoiceMasterRepository.Update(entity);
        }
        #endregion
    }
}
