using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fuhrpark.Data.Mappings
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Mobile).HasMaxLength(100);

            builder.Property(x => x.CreateDate).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
