using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data.Mapping.BranchMaster
{
    public partial class BranchMap : WMSEntityTypeConfiguration<Branch>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable(MappingDefaults.BranchTableName, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }

        #endregion
    }
}
