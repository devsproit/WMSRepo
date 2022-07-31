using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Security;

namespace WMS.Data.Mapping.Security
{
    public partial class PermissionMasterMap : WMSEntityTypeConfiguration<PermissionMaster>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<PermissionMaster> builder)
        {
            builder.ToTable(nameof(PermissionMaster));
            builder.HasKey(master => master.Id);
            builder.Property(master => master.Permission).IsRequired();
            base.Configure(builder);
        }
        #endregion
    }
}
