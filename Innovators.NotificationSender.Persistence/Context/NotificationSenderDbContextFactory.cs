using Microsoft.EntityFrameworkCore;
using Innovators.NotificationSender.Persistence.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
   public class NotificationSenderDbContextFactory: DesignTimeDbContextFactoryBase<NotificationSenderDbContext>
    {
        protected override NotificationSenderDbContext CreateNewInstance(DbContextOptions<NotificationSenderDbContext> options)
        {
            return new NotificationSenderDbContext(options);
        }
    }
}
