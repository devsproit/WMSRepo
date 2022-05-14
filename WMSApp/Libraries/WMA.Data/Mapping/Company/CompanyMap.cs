using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
namespace WMS.Data.Mapping.Company
{
    public partial class CompanyMap:WMSEntityTypeConfiguration<CompanyDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<CompanyDb> builder)
        {
            builder.ToTable(MappingDefaults.CompanyTable,"WMS");
            builder.HasKey(x=>x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
