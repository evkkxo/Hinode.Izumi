﻿using Hinode.Izumi.Data.Models;
using Hinode.Izumi.Framework.EF;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hinode.Izumi.Data.Configurations
{
    public class BannerConfiguration : EntityTypeConfigurationBase<Banner>
    {
        public override void Configure(EntityTypeBuilder<Banner> b)
        {
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Rarity).IsRequired();
            b.Property(x => x.Url).IsRequired();
            b.Property(x => x.Price).IsRequired();

            base.Configure(b);
        }
    }
}
