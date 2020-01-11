using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
   public class NotificationSenderInitializer
    {

        public static void Initialize (NotificationSenderDbCondetxt context)
        {
            var initializer = new NotificationSenderInitializer();
            initializer.SeedAllData(context);
        }


        public void SeedAllData(NotificationSenderDbCondetxt context)
        {
            context.Database.EnsureCreated();

        }
    }
}
