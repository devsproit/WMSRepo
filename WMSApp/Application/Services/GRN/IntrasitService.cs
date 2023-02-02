using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model;
namespace Application.Services.GRN
{
    public class IntrasitService : IIntrasitService
    {
        #region Fields
        private readonly IRepository<IntrasitDb> _intrasitRepository;

        #endregion

        #region Ctor
        public IntrasitService(IRepository<IntrasitDb> intrasitRepository)
        {
            _intrasitRepository = intrasitRepository;
        }
        #endregion

        #region Methods

        public virtual IPagedList<IntrasitDb> GetPendingPO(string branchCode, string pono, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(pono))
                {
                    var query = from x in _intrasitRepository.Table
                                select x;
                    query = query.Where(x => x.Login_Branch == branchCode && x.IsGrn == false);

                    if (pono == "0")
                    {

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pono))
                        {
                            query = query.Where(x => x.PurchaseOrder == pono);
                        }
                    }



                    var result = new PagedList<IntrasitDb>(query, pageIndex, pageSize);
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

        public virtual IPagedList<IntrasitDb> GetDonePO(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _intrasitRepository.Table
                        select x;
            query = query.Where(x => x.Login_Branch == branchCode && x.IsGrn == true);

            query = query.OrderByDescending(x => x.Id);
            return new PagedList<IntrasitDb>(query, pageIndex, pageSize);
        }

        public virtual IntrasitDb GetById(int id)
        {
            return _intrasitRepository.GetById(id);
        }

        public virtual void Update(IntrasitDb entitiy)
        {
            _intrasitRepository.Update(entitiy);
        }
        #endregion


    }
}
