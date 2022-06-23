using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.PS;

namespace WMS.Data.Mapping.PS
{
    public partial class PickSlipMasterMap:WMSEntityTypeConfiguration<PickSlipMaster>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<PickSlipMaster> builder)
        {
            builder.ToTable(nameof(PickSlipMaster));
            builder.HasKey(key => key.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
