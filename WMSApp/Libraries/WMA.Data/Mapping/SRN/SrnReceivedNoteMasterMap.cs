using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.SRN;
namespace WMS.Data.Mapping.SRN
{
    public partial class SrnReceivedNoteMasterMap:WMSEntityTypeConfiguration<SrnReceivedNoteMaster>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<SrnReceivedNoteMaster> builder)
        {
            builder.ToTable(nameof(SrnReceivedNoteMaster));
            builder.HasKey(key=>key.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
