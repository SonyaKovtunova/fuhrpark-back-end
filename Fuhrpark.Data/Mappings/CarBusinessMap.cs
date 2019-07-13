using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarBusinessMap : IEntityTypeConfiguration<CarBusiness>
    {
        public void Configure(EntityTypeBuilder<CarBusiness> builder)
        {
            builder.ToTable("tbCarCore");

            builder.Property(x => x.Location).HasMaxLength(255).HasColumnName("car_location");

            builder.Property(x => x.UserId).HasColumnName("id_user_car");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("car_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("car_date_update");

            builder.HasOne(x => x.User).WithMany(x => x.Cars).HasForeignKey(x => x.UserId);
        }
    }
}
