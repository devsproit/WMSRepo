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

        public virtual IPagedList<IntrasitDb> GetPendingPO(string branchCode, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _intrasitRepository.Table
                        select x;
            query = query.Where(x => x.Login_Branch == branchCode && x.IsGrn == false);
            var result = new PagedList<IntrasitDb>(query, pageIndex, pageSize);
            return result;
        }

        #endregion


    }
}
