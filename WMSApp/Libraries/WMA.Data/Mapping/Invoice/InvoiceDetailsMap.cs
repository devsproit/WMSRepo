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
    public partial class InvoiceDetailsMap : WMSEntityTypeConfiguration<InvoiceDetails>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<InvoiceDetails> builder)
        {
            builder.ToTable(nameof(InvoiceDetails));

            builder.HasKey(x => x.Id);
            builder.HasOne(details => details.InvoiceMaster)
                .WithMany(master => master.InvoiceDetails)
                .HasForeignKey(details => details.InvoiceMasterId)
                .IsRequired();
            base.Configure(builder);
        }
        #endregion
    }
}
