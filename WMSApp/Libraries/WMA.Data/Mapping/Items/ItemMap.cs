using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Model;

namespace WMS.Data.Mapping.Items
{
    public partial class ItemMap:WMSEntityTypeConfiguration<ItemDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<ItemDb> builder)
        {
            builder.ToTable("Item");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }

        #endregion
    }
}
