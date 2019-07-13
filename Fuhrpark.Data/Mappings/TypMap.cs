using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class TypMap : IEntityTypeConfiguration<Typ>
    {
        public void Configure(EntityTypeBuilder<Typ> builder)
        {
            builder.ToTable("tbTyp");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_typ_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("typ_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("typ_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("typ_date_update");
        }
    }
}
