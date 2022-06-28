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
    public class ServiceOrderPOMap : WMSEntityTypeConfiguration<ServiceOrderPODb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<ServiceOrderPODb> builder)
        {
            builder.ToTable(MappingDefaults.ServiceOrderTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
