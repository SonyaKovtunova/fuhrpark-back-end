using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("tbCarCore");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_car_core");

            builder.Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(255).HasColumnName("car_reg_number");

            builder.Property(x => x.TypId).IsRequired().HasColumnName("id_typ_car");

            builder.Property(x => x.ManufacturerId).IsRequired().HasColumnName("id_manufacturer_car");

            builder.Property(x => x.Model).HasMaxLength(255).HasColumnName("car_model");

            builder.Property(x => x.Color).HasMaxLength(255).HasColumnName("car_color");

            builder.Property(x => x.ChassisNumber).HasMaxLength(255).HasColumnName("car_chassis_number");

            builder.Property(x => x.Decommissioned).HasColumnName("car_decommissioned");

            builder.HasOne(x => x.Typ).WithMany(x => x.Cars).HasForeignKey(x => x.TypId);

            builder.HasOne(x => x.Manufacturer).WithMany(x => x.Cars).HasForeignKey(x => x.ManufacturerId);

            builder.HasOne(x => x.CarSpec).WithOne(x => x.Car).HasForeignKey<CarSpec>(x => x.Id);

            builder.HasOne(x => x.CarBusiness).WithOne(x => x.Car).HasForeignKey<CarBusiness>(x => x.Id);
        }
    }
}
