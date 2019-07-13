using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class FuelMap : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder.ToTable("tbFuel");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_fuel_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("fuel_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("fuel_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("fuel_date_update");
        }
    }
}
