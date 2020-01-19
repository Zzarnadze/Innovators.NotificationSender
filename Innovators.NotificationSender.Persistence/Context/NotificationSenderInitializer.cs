using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
   public class NotificationSenderInitializer
    {

        public static void Initialize (NotificationSenderDbContext context)
        {
            var initializer = new NotificationSenderInitializer();
            initializer.SeedAllData(context);
        }


        public void SeedAllData(NotificationSenderDbContext context)
        {
            context.Database.EnsureCreated();

        }
    }
}
