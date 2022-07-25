using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Invoice;


namespace WMS.Data.Mapping.Invoice
{
    public partial class InvoiceMasterMap:WMSEntityTypeConfiguration<InvoiceMaster>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<InvoiceMaster> builder)
        {
            builder.ToTable(nameof(InvoiceMaster));
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
