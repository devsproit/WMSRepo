using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

namespace Application.Services.PO
{
    public class SrnPo:ISrnPo
    {
        #region Fields
        private readonly IRepository<SRNPoDb> _srnPoRepository;

        #endregion

        #region Ctor
        public SrnPo(IRepository<SRNPoDb> srnPoRepository)
        {
            _srnPoRepository = srnPoRepository;
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
