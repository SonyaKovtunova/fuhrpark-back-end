using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class GearOilMap : IEntityTypeConfiguration<GearOil>
    {
        public void Configure(EntityTypeBuilder<GearOil> builder)
        {
            builder.ToTable("tbGearOil");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_gear_oil_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("gear_oil_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("gear_oil_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("gear_oil_date_update");
        }
    }
}
