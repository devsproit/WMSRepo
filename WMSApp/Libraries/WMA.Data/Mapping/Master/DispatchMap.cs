using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.Masters;

namespace WMS.Data.Mapping.Master
{
    public partial class DispatchMap:WMSEntityTypeConfiguration<Dispatch>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<Dispatch> builder)
        {
            builder.ToTable(nameof(Dispatch));
            builder.HasKey(key => key.Id);
            base.Configure(builder);
        }
        #endregion

    }
}
