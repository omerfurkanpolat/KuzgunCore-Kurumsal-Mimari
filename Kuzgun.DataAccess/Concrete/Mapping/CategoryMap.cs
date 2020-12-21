using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class CategoryMap: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryName).HasColumnName("CategoryName");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated").IsRequired();
            builder.Property(c => c.IsDeleted).HasColumnName("IsDeleted").IsRequired();

        }
    }
}
