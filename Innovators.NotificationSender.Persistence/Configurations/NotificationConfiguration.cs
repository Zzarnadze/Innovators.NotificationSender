using Innovators.NotificationSender.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Configurations
{
  public  class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void  Configure (EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.Property(e => e.Sender).IsRequired();

            builder.Property(e => e.Receiver).IsRequired();

            builder.Property(e => e.Subject).IsRequired();

            builder.Property(e => e.Body).IsRequired();

            builder.Property(e => e.NotificationTypeId).IsRequired();

            builder.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreationDate).IsRequired();

            builder.HasOne(e => e.NotificationType)
                .WithMany(e => e.Notifications)
                .HasForeignKey(e => e.NotificationTypeId)
                .HasConstraintName("FK_Files_NotificationType_NotificationTypeId");
        }
    }
}
