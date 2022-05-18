using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Model.Masters;

namespace WMS.Data.Mapping.WarehouseMaster
{
    public partial class WarehouseMap:WMSEntityTypeConfiguration<Warehouse>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable(MappingDefaults.WarehouseTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
