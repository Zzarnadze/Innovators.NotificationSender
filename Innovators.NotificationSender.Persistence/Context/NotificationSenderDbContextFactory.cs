using Microsoft.EntityFrameworkCore;
using Innovators.NotificationSender.Persistence.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
    /// <summary>
    /// Notification sender db context factory
    /// </summary>
    public class NotificationSenderDbContextFactory: DesignTimeDbContextFactoryBase<NotificationSenderDbContext>
    {
        /// <summary>
        /// Creates new instance of notification sender db context
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected override NotificationSenderDbContext CreateNewInstance(DbContextOptions<NotificationSenderDbContext> options)
        {
            return new NotificationSenderDbContext(options);
        }
    }
}
