using Domain.Model.PO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data.Mapping.Po
{
    public class StockTransferPOMap: WMSEntityTypeConfiguration<StockTransferPoDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<StockTransferPoDb> builder)
        {
            builder.ToTable(MappingDefaults.StockTransferTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
