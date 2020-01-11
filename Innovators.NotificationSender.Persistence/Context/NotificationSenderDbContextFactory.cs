using Microsoft.EntityFrameworkCore;
using Innovators.NotificationSender.Persistence.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
   public class NotificationSenderDbContextFactory: DesignTimeDbContextFactoryBase<NotificationSenderDbCondetxt>
    {
        protected override NotificationSenderDbCondetxt CreateNewInstance(DbContextOptions<NotificationSenderDbCondetxt> options)
        {
            return new NotificationSenderDbCondetxt(options);
        }
    }
}
