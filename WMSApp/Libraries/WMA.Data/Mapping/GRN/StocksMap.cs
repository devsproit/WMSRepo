using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.GRN;
using Domain.Model.StockManagement;
namespace WMS.Data.Mapping.GRN
{
    public partial class StocksMap : WMSEntityTypeConfiguration<ItemStock>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<ItemStock> builder)
        {
            builder.ToTable(nameof(Stocks));
            builder.HasKey(key => key.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
