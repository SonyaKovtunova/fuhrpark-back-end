using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarSubgroupInCarGroupMap : IEntityTypeConfiguration<CarSubgroupInCarGroup>
    {
        public void Configure(EntityTypeBuilder<CarSubgroupInCarGroup> builder)
        {
            builder.HasKey(x => new { x.CarSubgroupId, x.CarGroupId });

            builder.HasOne(x => x.CarSubgroup).WithMany().HasForeignKey(x => x.CarSubgroupId);

            builder.HasOne(x => x.CarGroup).WithMany(x => x.CarSubgroupInCarGroups).HasForeignKey(x => x.CarGroupId);
        }
    }
}
