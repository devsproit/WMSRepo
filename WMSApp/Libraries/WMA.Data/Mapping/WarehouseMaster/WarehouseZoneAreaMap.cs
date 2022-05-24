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
            builder.ToTable(nameof(WarehouseZoneArea));
            builder.HasKey(key => key.Id);
            builder.HasOne(warehousezone => warehousezone.Warehouse)
                .WithMany(warehouse => warehouse.WarehouseZoneAreas)
                .HasForeignKey(fkey => fkey.WarehouseId)
                .IsRequired();
            builder.HasOne(warehousezonearea => warehousezonearea.WarehouseZone)
                .WithMany(warehousezone => warehousezone.WarehouseZoneAreas)
                .HasForeignKey(fkey => fkey.ZoneId)
                .IsRequired();

            base.Configure(builder);
        }
        #endregion
    }
}
