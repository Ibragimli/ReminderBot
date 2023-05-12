using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReminderBot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Data.Configuration
{
     
     public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.Property(x => x.To).HasMaxLength(45).IsRequired(true);
            builder.Property(x => x.Content).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Method).HasMaxLength(10).IsRequired(true);
            builder.Property(x => x.SendAt).IsRequired(true);
            builder.Property(e => e.SendAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()") // İsteğe bağlı olarak varsayılan değer atayabilirsiniz
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                    .HasColumnType("Date")
                    .HasComment("The date value.");

            builder.HasCheckConstraint("CK_MyEntity_Date", "Date >= GETDATE()"); // Kısıtlama ekleme

        }
    }
}
