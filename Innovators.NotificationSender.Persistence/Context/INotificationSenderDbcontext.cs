using Microsoft.EntityFrameworkCore;
using Innovators.NotificationSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Innovators.NotificationSender.Persistence.Context
{
    interface INotificationSenderDbcontext
    {
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<MailTemplate> MailTemplates { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
