using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarGroupMap : IEntityTypeConfiguration<CarGroup>
    {
        public void Configure(EntityTypeBuilder<CarGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

            builder.Property(x => x.CreateDate).IsRequired();
        }
    }
}
