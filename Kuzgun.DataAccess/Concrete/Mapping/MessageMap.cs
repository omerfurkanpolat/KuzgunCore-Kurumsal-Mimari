using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuzgun.DataAccess.Concrete.Mapping
{
    public class MessageMap: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(m => m.MessageId);
            builder.Property(m => m.FullName).HasColumnName("FullName");
            builder.Property(m => m.MessageHeader).HasColumnName("MessageHeader");
            builder.Property(m => m.Email).HasColumnName("Email");
            builder.Property(m => m.MessageBody).HasColumnName("MessageBody");
        }
    }
}
