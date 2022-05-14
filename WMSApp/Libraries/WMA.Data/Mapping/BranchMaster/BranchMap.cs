using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data.Mapping.BranchMaster
{
    public partial class BranchMap:WMSEntityTypeConfiguration<BranchDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<BranchDb> builder)
        {
            builder.ToTable(MappingDefaults.BranchTableName);
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }

        #endregion
    }
}
