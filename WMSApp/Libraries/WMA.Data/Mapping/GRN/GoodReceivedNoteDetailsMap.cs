using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.GRN;
namespace WMS.Data.Mapping.GRN
{
    public partial class GoodReceivedNoteDetailsMap:WMSEntityTypeConfiguration<GoodReceivedNoteDetails>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<GoodReceivedNoteDetails> builder)
        {
            builder.ToTable(nameof(GoodReceivedNoteDetails));
            builder.HasKey(key => key.Id);
            builder.HasOne(details => details.GoodReceivedNoteMaster)
                .WithMany(master => master.GoodReceivedNoteDetails)
                .HasForeignKey(fkey => fkey.GRNId)
                .IsRequired();

            base.Configure(builder);
        }
        #endregion
    }
}
