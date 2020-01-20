using Innovators.NotificationSender.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Configurations
{
    public class SmsSettingConfiguration : IEntityTypeConfiguration<SmsSetting>
    {
        public void Configure(EntityTypeBuilder<SmsSetting> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.ServiceId).IsRequired();

            builder.Property(x => x.ServiceRequestUrl).IsRequired();

            builder.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreationDate).IsRequired();
        }
    }
}
