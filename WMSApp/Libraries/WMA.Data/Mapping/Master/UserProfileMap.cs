using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Model.Masters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WMS.Data.Mapping.Master
{
    public class UserProfileMap : WMSEntityTypeConfiguration<UserProfile>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable(nameof(UserProfile));
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }
        #endregion
    }
}
