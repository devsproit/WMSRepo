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
    public class SrnPo : ISrnPo
    {
        #region Fields
        private readonly IRepository<SRNPoDb> _srnPoRepository;
        private readonly IRepository<PurchaseOrderDb> _poRepository;
        #endregion

        #region Ctor
        public SrnPo(IRepository<SRNPoDb> srnPoRepository, IRepository<PurchaseOrderDb> poRepository)
        {
            _srnPoRepository = srnPoRepository;
            _poRepository = poRepository;
        }

        //public SRNPoDb GetAll(string PoNo)
        //{
        //   return _srnPoRepository.GetById(PoNo);
        //}

        public SRNPoDb GetById(int id)
        {
            return _srnPoRepository.GetById(id);
        }

        public IPagedList<SRNPoDb> GetSrnDetails(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(pono))
                {
                    var query = from x in _srnPoRepository.Table
                                join y in _poRepository.Table
                                on x.PONumber equals y.PONumber
                                where y.BranchCode == branchCode
                                && x.PONumber == pono
                                select x;
                    var result = new PagedList<SRNPoDb>(query, pageIndex, pageSize);
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

        public void Insert(SRNPoDb salePoDb)
        {
            _srnPoRepository.Insert(salePoDb);
        }

        public void Update(SRNPoDb salePoDb)
        {
            _srnPoRepository.Update(salePoDb);
        }



        #endregion
    }
}
