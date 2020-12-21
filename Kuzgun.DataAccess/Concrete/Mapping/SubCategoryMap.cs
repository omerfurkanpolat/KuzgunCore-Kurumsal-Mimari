using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class SubCategoryMap : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");
            builder.HasKey(p => p.SubCategoryId);
            builder.Property(p => p.CategoryId).HasColumnName("CategoryId");
            builder.Property(p => p.SubCategoryName).HasColumnName("SubCategoryName");
            builder.Property(p => p.IsDeleted).HasColumnName("IsDeleted");
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated");


            builder.HasOne(s => s.Category).WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();





        }
    }
}
