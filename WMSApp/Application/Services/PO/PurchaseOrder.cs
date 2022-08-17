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
    public partial class PurchaseOrder : IPurchaseOrder
    {
        #region Fields
        private readonly IRepository<PurchaseOrderDb> _poRepository;
        private readonly IRepository<SRNPoDb> _srnRepository;
        #endregion

        #region Ctor
        public PurchaseOrder(IRepository<PurchaseOrderDb> poRepository, IRepository<SRNPoDb> SrnRepository)
        {
            _poRepository = poRepository;
            _srnRepository = SrnRepository;
        }


        #endregion

        #region Methods

        
        public void Insert(PurchaseOrderDb entiry)
        {
            _poRepository.Insert(entiry);
        }

        public void Update(PurchaseOrderDb entiry)
        {
            _poRepository.Update(entiry);
        }
        public PurchaseOrderDb GetById(int id)
        {
            return _poRepository.GetById(id);
        }
        public virtual IPagedList<PurchaseOrderDb> GetPurchaseOrders(string branchCode, string category, bool status = true, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _poRepository.Table
                        where x.BranchCode == branchCode && x.POCategory == category
                        select x;
            query = query.Where(x => x.ProcessStatus == status);
            query = query.OrderBy(x => x.Id);
            var result = new PagedList<PurchaseOrderDb>(query, pageIndex, pageSize);
            return result;

        }
        public virtual IPagedList<PurchaseOrderDb> GetPendingPO(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(pono))
                {
                    var query = from x in _poRepository.Table
                                select x;
                    query = query.Where(x => x.BranchCode == branchCode);

                    if (pono == "0")
                    {
                        query = query.Where(x =>x.POCategory == "SRN PO");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pono))
                        {
                            query = query.Where(x => x.PONumber == pono && x.POCategory == "SRN PO");
                        }
                    }



                    var result = new PagedList<PurchaseOrderDb>(query, pageIndex, pageSize);
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public IPagedList<PurchaseOrderDb> GetPurchaseOrders(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(pono))
                {
                    var query = from x in _poRepository.Table
                                select x;
                    query = query.Where(x => x.BranchCode == branchCode);

                    if (pono == "0")
                    {

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pono))
                        {
                            query = query.Where(x => x.PONumber == pono && x.POCategory =="SRN PO");
                        }
                    }



                    var result = new PagedList<PurchaseOrderDb>(query, pageIndex, pageSize);
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

       

        #endregion
    }
}
