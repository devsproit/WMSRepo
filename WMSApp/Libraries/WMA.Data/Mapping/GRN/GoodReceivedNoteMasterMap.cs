using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.GRN;
namespace WMS.Data.Mapping.GRN
{
    public partial class GoodReceivedNoteMasterMap:WMSEntityTypeConfiguration<GoodReceivedNoteMaster>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<GoodReceivedNoteMaster> builder)
        {
            builder.ToTable(nameof(GoodReceivedNoteMaster));
            builder.HasKey(key=>key.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
