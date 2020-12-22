using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class PostMap: IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts" );
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CategoryId).HasColumnName("CategoryId");
            builder.Property(p => p.SubCategoryId).HasColumnName("SubCategoryId");
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated");
            builder.Property(p => p.Title).HasColumnName("Title");
            builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
            builder.Property(p => p.Body).HasColumnName("Body");
            builder.Property(p => p.IsPublished).HasColumnName("IsPublished");

            builder.HasOne(p => p.Category).WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();

            builder.HasOne(p => p.SubCategory).WithMany(c => c.Posts)
                .HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Restrict).IsRequired();

            builder.HasOne(p => p.User).WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
