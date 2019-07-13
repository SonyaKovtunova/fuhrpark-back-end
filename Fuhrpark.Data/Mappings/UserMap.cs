using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbUser");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(8).HasColumnName("id_user_car");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasColumnName("user_name");

            builder.Property(x => x.CreateDate).IsRequired().HasColumnName("user_date_create");

            builder.Property(x => x.UpdateDate).HasColumnName("user_date_update");
        }
    }
}
