using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.SubItemMapping;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data.Mapping.Items
{
    public partial class SubItemWarehouseMappingMap:WMSEntityTypeConfiguration<SubItemWareHouseMapping>
    {

        #region Methods
        public override void Configure(EntityTypeBuilder<SubItemWareHouseMapping> builder)
        {
            builder.ToTable(nameof(SubItemWareHouseMapping));
            builder.HasKey(x => x.Id);

            base.Configure(builder);
        }
        #endregion
    }
}
