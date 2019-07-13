using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class EngineOilMap : IEntityTypeConfiguration<EngineOil>
    {
        public void Configure(EntityTypeBuilder<EngineOil> builder)
        {
            builder.ToTable("tbEngineOil");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_engine_oil_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("engine_oil_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("engine_oil_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("engine_oil_date_update");
        }
    }
}
