using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.LastActive).IsRequired();

        }
    }
}
