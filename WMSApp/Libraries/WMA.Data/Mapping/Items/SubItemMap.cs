using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model;


namespace WMS.Data.Mapping.Items
{
    public partial class SubItemMap:WMSEntityTypeConfiguration<SubItemDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<SubItemDb> builder)
        {
            builder.ToTable("SubItem");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
