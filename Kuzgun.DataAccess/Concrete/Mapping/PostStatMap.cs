using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class PostStatMap: IEntityTypeConfiguration<PostStat>
    {
        public void Configure(EntityTypeBuilder<PostStat> builder)
        {
            builder.ToTable("PostStats");
            builder.HasKey(p => p.PostStatId);
            builder.Property(p => p.PostId).HasColumnName("PostId");
            builder.Property(p => p.Claps).HasColumnName("Claps");
            builder.Property(p => p.DateAdded).HasColumnName("DateAdded");
            builder.Property(p => p.Views).HasColumnName("Views");

            builder.HasOne(ps => ps.Post).WithOne(p => p.PostStat)
                .HasForeignKey<PostStat>(p => p.PostId);

        }
    }
}
