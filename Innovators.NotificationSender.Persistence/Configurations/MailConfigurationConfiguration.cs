using Innovators.NotificationSender.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Configurations
{
   public class MailConfigurationConfiguration :IEntityTypeConfiguration<MailConfiguration>
    {
        public void Configure (EntityTypeBuilder<MailConfiguration> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.Email).IsRequired();

            builder.Property(e => e.UserName).IsRequired();

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.Host).IsRequired();

            builder.Property(e => e.Port).IsRequired();

            builder.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreationDate).IsRequired();

        }
    }
}
