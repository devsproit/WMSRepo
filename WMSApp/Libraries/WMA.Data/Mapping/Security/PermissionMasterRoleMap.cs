using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Security;

namespace WMS.Data.Mapping.Security
{
    public class PermissionMasterRoleMap : WMSEntityTypeConfiguration<PermissionMaster_Role_Mapping>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<PermissionMaster_Role_Mapping> builder)
        {
            builder.ToTable(nameof(PermissionMaster_Role_Mapping));
            builder.HasKey(key => key.Id);
            builder.HasOne(pm => pm.PermissionMaster)
                .WithMany(pr => pr.PermissionMaster_Role_Mappings)
                .HasForeignKey(fpr => fpr.Permission_Id)
                .IsRequired();
            base.Configure(builder);
        }
        #endregion
    }
}
