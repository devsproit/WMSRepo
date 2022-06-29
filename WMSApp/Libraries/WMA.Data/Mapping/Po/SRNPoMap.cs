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
    public class SRNPoMap : WMSEntityTypeConfiguration<SRNPoDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<SRNPoDb> builder)
        {
            builder.ToTable(MappingDefaults.SRnTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
