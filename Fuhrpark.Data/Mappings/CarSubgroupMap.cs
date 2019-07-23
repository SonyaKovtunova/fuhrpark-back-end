using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarSubgroupMap : IEntityTypeConfiguration<CarSubgroup>
    {
        public void Configure(EntityTypeBuilder<CarSubgroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

            builder.Property(x => x.CreateDate).IsRequired();
        }
    }
}
