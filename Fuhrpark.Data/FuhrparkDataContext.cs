using Anthill.Common.Data;
using Anthill.Common.Data.Contracts;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data
{
    public class FuhrparkDataContext : AbstractDataContext
    {
        public FuhrparkDataContext(IFuhrparkConnectionConfiguration configuration) : base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies()
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarBusinessMap());
            modelBuilder.ApplyConfiguration(new CarMap());
            modelBuilder.ApplyConfiguration(new CarSpecMap());
            modelBuilder.ApplyConfiguration(new EngineOilMap());
            modelBuilder.ApplyConfiguration(new FuelMap());
            modelBuilder.ApplyConfiguration(new GearOilMap());
            modelBuilder.ApplyConfiguration(new ManufacturerMap());
            modelBuilder.ApplyConfiguration(new TypMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new CarInCarSubgroupMap());
            modelBuilder.ApplyConfiguration(new CarSubgroupMap());
            modelBuilder.ApplyConfiguration(new CarSubgroupInCarGroupMap());
            modelBuilder.ApplyConfiguration(new CarGroupMap());
        }
    }
}
