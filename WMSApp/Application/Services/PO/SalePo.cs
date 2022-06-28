using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

namespace Application.Services.PO
{
    public partial class SalePo : ISalePo
    {
        #region Fields
        private readonly IRepository<SalePoDb> _salePoRepository;

        #endregion

        #region Ctor
        public SalePo(IRepository<SalePoDb> salePoRepository)
        {
            _salePoRepository = salePoRepository;
        }
        public void Insert(SalePoDb salePoDb)
        {
            _salePoRepository.Insert(salePoDb);
        }

        public void Update(SalePoDb salePoDb)
        {
            _salePoRepository.Update(salePoDb);
        }
        #endregion
    }
}
