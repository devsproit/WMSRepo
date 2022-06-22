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
    public partial class TempPickSlipDetailsMap : WMSEntityTypeConfiguration<TempPickSlipDetails>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<TempPickSlipDetails> builder)
        {
            builder.ToTable(nameof(TempPickSlipDetails));
            builder.HasKey(key => key.Id);
            base.Configure(builder);
        }

        #endregion
    }
}
