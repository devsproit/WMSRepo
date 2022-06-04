using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WMS.Data.Mapping.Process
{
    public partial class IntrasitMap : WMSEntityTypeConfiguration<IntrasitDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<IntrasitDb> builder)
        {
            builder.ToTable("Intrasit", "WMS");
            builder.HasKey(x => x.Id);

            base.Configure(builder);
        }
        #endregion
    }
}
