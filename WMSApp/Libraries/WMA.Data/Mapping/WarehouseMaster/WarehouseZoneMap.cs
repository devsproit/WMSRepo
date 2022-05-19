using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WMS.Data.Mapping.WarehouseMaster
{
    public partial class WarehouseZoneMap : WMSEntityTypeConfiguration<WarehouseZone>
    {
        #region methods
        public override void Configure(EntityTypeBuilder<WarehouseZone> builder)
        {
            builder.ToTable(nameof(WarehouseZone));
            builder.HasKey(x => x.Id);
            builder.HasOne(warehousezone => warehousezone.Warehouse)
                .WithMany(warehouse => warehouse.WarehouseZones)
                .HasForeignKey(fkey => fkey.WarehouseId)
                .IsRequired();
            base.Configure(builder);
        }
        #endregion
    }
}
