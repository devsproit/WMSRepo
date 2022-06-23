using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.PS;
namespace WMS.Data.Mapping.PS
{
    public partial class PickSlipDetailsMap : WMSEntityTypeConfiguration<PickSlipDetails>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<PickSlipDetails> builder)
        {
            builder.ToTable(nameof(PickSlipDetails));
            builder.HasKey(key => key.Id);
            builder.HasOne(pickslipDetails => pickslipDetails.PickSlipMaster)
                .WithMany(master => master.PickSlipDetails)
                .HasForeignKey(fkey => fkey.PickSlipId)
                .IsRequired();
            base.Configure(builder);
        }

        #endregion
    }
}
