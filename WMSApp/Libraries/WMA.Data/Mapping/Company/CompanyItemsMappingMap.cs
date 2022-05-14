using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Data;
using Domain.Model.CompanyMaster;

namespace WMS.Data.Mapping.Company
{
    public class CompanyItemsMappingMap : WMSEntityTypeConfiguration<CompanyItemsMapping>
    {

        #region Methods
        public override void Configure(EntityTypeBuilder<CompanyItemsMapping> builder)
        {
            builder.ToTable(nameof(CompanyItemsMapping));
            builder.HasKey(x => x.Id);
            builder.HasOne(mapping => mapping.Company)
                .WithMany(company => company.CompanyItemsMappings)
                .HasForeignKey(mapping => mapping.CompanyId)
                .IsRequired();

            builder.HasOne(mapping => mapping.Item)
                .WithMany(items => items.CompanyItemsMappings)
                .HasForeignKey(mapping => mapping.ItemId)
                .IsRequired();

            base.Configure(builder);
        }
        #endregion
    }
}
