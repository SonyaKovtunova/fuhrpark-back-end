using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarInCarSubgroupMap : IEntityTypeConfiguration<CarInCarSubgroup>
    {
        public void Configure(EntityTypeBuilder<CarInCarSubgroup> builder)
        {
            builder.HasKey(x => new { x.CarId, x.CarSubgroupId });

            builder.HasOne(x => x.Car).WithMany().HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.CarSubgroup).WithMany(x => x.CarInCarSubgroups).HasForeignKey(x => x.CarSubgroupId);
        }
    }
}
