using Innovators.NotificationSender.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Configurations
{
   public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.Property(e => e.Name).IsRequired();
        }
    }
}
