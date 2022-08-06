using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Model.SRN;
namespace WMS.Data.Mapping.SRN
{
    public partial class SrnNoteDetailsMap:WMSEntityTypeConfiguration<SrnNoteDetails>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<SrnNoteDetails> builder)
        {
            builder.ToTable(nameof(SrnNoteDetails));
            builder.HasKey(key => key.Id);
            builder.HasOne(details => details.SrnReceivedNoteMaster)
                .WithMany(master => master.SrnNoteDetails)
                .HasForeignKey(fkey => fkey.SRNId)
                .IsRequired();

            base.Configure(builder);
        }
        #endregion
    }
}
