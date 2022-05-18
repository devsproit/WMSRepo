using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Model.Masters;
namespace WMS.Data.Mapping.WarehouseMaster
{
    public partial class WarehouseZoneAreaMap : WMSEntityTypeConfiguration<WarehouseZoneArea>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<WarehouseZoneArea> builder)
        {
            builder.ToTable(nameof(WarehouseZoneAreaMap));
            builder.HasKey(key => key.Id);
            builder.HasOne(warehousezone => warehousezone.Warehouse)
                .WithMany(warehouse => warehouse.WarehouseZoneAreas)
                .HasForeignKey(fkey => fkey.WarehouseId)
                .IsRequired();

            base.Configure(builder);
        }
        #endregion
    }
}
