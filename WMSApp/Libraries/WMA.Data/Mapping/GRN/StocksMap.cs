using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.GRN;

namespace WMS.Data.Mapping.GRN
{
    public partial class StocksMap : WMSEntityTypeConfiguration<Stocks>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<Stocks> builder)
        {
            builder.ToTable(nameof(Stocks));
            builder.HasKey(key => key.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
