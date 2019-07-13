using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class ManufacturerMap : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("tbManufacturer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_manufacturer_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("manufacturer_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("manufacturer_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("manufacturer_date_update");
        }
    }
}
