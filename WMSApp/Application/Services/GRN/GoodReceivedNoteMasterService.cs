using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.GRN;
namespace Application.Services.GRN
{
    public partial class GoodReceivedNoteMasterService : IGoodReceivedNoteMasterService
    {
        #region Fields
        private readonly IRepository<GoodReceivedNoteMaster> _grnMasterRepository;
        private readonly IRepository<GoodReceivedNoteDetails> _grnDetailsRepository;

        #endregion

        #region Ctor
        public GoodReceivedNoteMasterService(IRepository<GoodReceivedNoteMaster> grnMasterRepository, IRepository<GoodReceivedNoteDetails> grnDetailsRepository)
        {
            _grnMasterRepository = grnMasterRepository;
            _grnDetailsRepository = grnDetailsRepository;
        }


        #endregion

        #region Methods
        public virtual IPagedList<GoodReceivedNoteMaster> GetAllMaster(string branch, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                var query = from x in _grnMasterRepository.Table
                            select x;
                query = query.Where(x => x.BranchCode == branch);
                query = query.OrderByDescending(x => x.Id);                
                var result = new PagedList<GoodReceivedNoteMaster>(query, pageIndex, pageSize);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual IPagedList<GoodReceivedNoteDetails> GetReport(DateTime fromdate, DateTime todate, string branch = "ALL",int pageIndex=0,int pageSize=int.MaxValue)
        {
            var query = from x in _grnDetailsRepository.Table
                        select x;
            query = query.Where(x => x.GoodReceivedNoteMaster.InvoiceDate.Date >= fromdate.Date && x.GoodReceivedNoteMaster.InvoiceDate.Date <= todate.Date);
            if(branch!="ALL")
            {
                query = query.Where(x => x.GoodReceivedNoteMaster.BranchCode == branch);
            }
            query = query.OrderByDescending(x => x.GoodReceivedNoteMaster.Id);
            var result = new PagedList<GoodReceivedNoteDetails>(query, pageIndex, pageSize);
            return result;
        }

        public virtual IPagedList<Stocks> GetStocks(string branchcode, string itemcode = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _grnDetailsRepository.Table
                        join y in _grnMasterRepository.Table
                        on x.GRNId equals y.Id
                        where y.BranchCode == branchcode
                        group x by x.SubItemCode into item
                        select new Stocks
                        {
                            ItemCode = item.Key,
                            Qty = item.Sum(x => x.Qty),
                            SubItemName = item.FirstOrDefault().SubItemName
                        };
            var result = new PagedList<Stocks>(query, pageIndex, pageSize);
            return result;
        }

        public virtual GoodReceivedNoteMaster GetbyId(int id)
        {
            return _grnMasterRepository.GetById(id);
        }

        public virtual void Insert(GoodReceivedNoteMaster entiry)
        {
            _grnMasterRepository.Insert(entiry);
        }

        public virtual void Update(GoodReceivedNoteMaster entiry)
        {
            _grnMasterRepository.Update(entiry);
        }
        #endregion
    }
}
