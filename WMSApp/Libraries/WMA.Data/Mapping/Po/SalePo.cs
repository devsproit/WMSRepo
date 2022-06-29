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
    public partial class SaleMap : WMSEntityTypeConfiguration<SalePoDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<SalePoDb> builder)
        {
            builder.ToTable(MappingDefaults.SalePoTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }

        #endregion
    }
}

