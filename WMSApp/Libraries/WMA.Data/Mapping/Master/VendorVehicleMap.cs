using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Masters;


namespace WMS.Data.Mapping.Master
{
    public partial class VendorVehicleMap : WMSEntityTypeConfiguration<VendorVehicle>
    {
        #region methods
        public override void Configure(EntityTypeBuilder<VendorVehicle> builder)
        {
            builder.ToTable(nameof(VendorVehicle));
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}