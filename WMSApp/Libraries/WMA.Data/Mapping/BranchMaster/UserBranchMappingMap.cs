
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Masters;
namespace WMS.Data.Mapping.BranchMaster
{
    public partial class UserBranchMappingMap:WMSEntityTypeConfiguration<UserBranchMapping>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<UserBranchMapping> builder)
        {
            builder.ToTable(nameof(UserBranchMapping));
            builder.HasKey(x => x.Id);
            base.Configure(builder);


        }
        #endregion

    }
}
