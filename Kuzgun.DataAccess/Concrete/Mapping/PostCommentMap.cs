using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class PostCommentMap: IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.ToTable("PostComments");
            builder.HasKey(p => p.PostCommentId);
            builder.HasIndex(p => p.UserId).IsUnique();
            builder.HasIndex(p => p.PostId).IsUnique();
            builder.Property(p => p.PostId).HasColumnName("PostId");
            
            builder.Property(p => p.Comment).HasColumnName("Comment");;
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated");

            builder.HasOne(pc => pc.Post).WithMany(p => p.PostComments)
                .HasForeignKey(p => p.PostId).OnDelete(DeleteBehavior.Restrict).IsRequired();

            builder.HasOne(pm => pm.User).WithMany(u => u.PostComments)
                .HasForeignKey(pm => pm.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();





        }
    }
}
