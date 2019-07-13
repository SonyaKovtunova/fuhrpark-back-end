using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class CarSpecMap : IEntityTypeConfiguration<CarSpec>
    {
        public void Configure(EntityTypeBuilder<CarSpec> builder)
        {
            builder.ToTable("tbCarCore");

            builder.Property(x => x.FuelId).IsRequired().HasColumnName("id_fuel_car");

            builder.Property(x => x.Performance).HasMaxLength(8).HasColumnName("car_performace");

            builder.Property(x => x.EngineDisplacement).HasMaxLength(8).HasColumnName("car_displacement");

            builder.Property(x => x.MaxSpeed).HasMaxLength(8).HasColumnName("car_max_speed");

            builder.Property(x => x.TotalWeight).HasMaxLength(8).HasColumnName("car_weight");

            builder.Property(x => x.EngineCode).HasMaxLength(255).HasColumnName("car_engine_code");

            builder.Property(x => x.EngineOilId).IsRequired().HasColumnName("id_engine_oil_car");

            builder.Property(x => x.GearOilId).IsRequired().HasColumnName("id_gear_oil_car");

            builder.Property(x => x.ProductionDate).HasColumnName("car_production_date");

            builder.Property(x => x.RegistrationDate).HasColumnName("car_registration_date");

            builder.Property(x => x.Catalyst).HasColumnName("car_catalyst");

            builder.Property(x => x.HybridDrive).HasColumnName("car_hybrid_drive");

            builder.HasOne(x => x.Fuel).WithMany(x => x.Cars).HasForeignKey(x => x.FuelId);

            builder.HasOne(x => x.EngineOil).WithMany(x => x.Cars).HasForeignKey(x => x.EngineOilId);

            builder.HasOne(x => x.GearOil).WithMany(x => x.Cars).HasForeignKey(x => x.GearOilId);
        }
    }
}
