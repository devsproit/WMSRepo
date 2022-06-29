﻿using Domain.Model.PO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Data.Mapping.Po
{
    public partial class poMap : WMSEntityTypeConfiguration<PurchaseOrderDb>
    {
        #region Methods
        public override void Configure(EntityTypeBuilder<PurchaseOrderDb> builder)
        {
            builder.ToTable(MappingDefaults.PurchaseOrderTable, "WMS");
            builder.HasKey(x => x.Id);
            base.Configure(builder);
        }

        #endregion
    }
}
