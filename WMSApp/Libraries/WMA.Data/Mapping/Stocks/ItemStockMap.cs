using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.StockManagement;
     
namespace WMS.Data.Mapping.Stocks
{
    public partial class ItemStockMap: WMSEntityTypeConfiguration<InventoryStock>
    {
        #region Method
        public override void Configure(EntityTypeBuilder<InventoryStock> builder)
        {
            builder.ToTable(nameof(InventoryStock));
            builder.HasKey(x => x.Id);  
            base.Configure(builder);
        }
        #endregion
    }
}
