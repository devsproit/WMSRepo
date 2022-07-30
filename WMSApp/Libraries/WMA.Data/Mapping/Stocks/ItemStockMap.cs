using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.StockManagement;
     

namespace WMS.Data.Mapping.Stocks
{
    public partial class ItemStockMap: WMSEntityTypeConfiguration<ItemStock>
    {
        #region Method
        public override void Configure(EntityTypeBuilder<ItemStock> builder)
        {
            builder.ToTable(nameof(ItemStock));
            builder.HasKey(x => x.Id);  
            base.Configure(builder);
        }
        #endregion
    }
}
