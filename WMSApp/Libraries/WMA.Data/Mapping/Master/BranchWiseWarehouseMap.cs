using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Masters;

namespace WMS.Data.Mapping.Master
{
    public partial class BranchWiseWarehouseMap:WMSEntityTypeConfiguration<BranchWiseWarehouse>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<BranchWiseWarehouse> builder)
        {
            builder.ToTable(nameof(BranchWiseWarehouse));
            builder.HasKey(key => key.Id);
            builder.HasOne(mapping => mapping.Warehouse)
                .WithMany(warehouse => warehouse.BranchWiseWarehouses)
                .HasForeignKey(fkey => fkey.WarehouseId)
                .IsRequired();
            builder.HasOne(mapping => mapping.Branch)
                .WithMany(mapping => mapping.BranchWiseWarehouses)
                .HasForeignKey(fkey => fkey.BranchId)
                .IsRequired();
            base.Configure(builder);
        }
        #endregion
    }
}
