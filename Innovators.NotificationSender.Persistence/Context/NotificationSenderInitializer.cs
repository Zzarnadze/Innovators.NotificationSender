using Innovators.NotificationSender.Common.Helpers.Utilities.Encryption;
using Innovators.NotificationSender.Domain.Entities;
using Innovators.NotificationSender.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innovators.NotificationSender.Persistence.Context
{
    /// <summary>
    /// Notification sender db initializer class
    /// </summary>
   public class NotificationSenderInitializer
    {
        /// <summary>
        /// Initializes the db context
        /// </summary>
        /// <param name="context">Db context</param>
        public static void Initialize (NotificationSenderDbContext context)
        {
            var initializer = new NotificationSenderInitializer();
            initializer.SeedAllData(context);
        }

        /// <summary>
        /// Seeds all data
        /// </summary>
        /// <param name="context">Db context</param>
        public void SeedAllData(NotificationSenderDbContext context)
        {
            context.Database.EnsureCreated();

            SeedNotificationTypes(context);

            SeedMailSettings(context);
        }

        /// <summary>
        /// Seeds notification types
        /// </summary>
        /// <param name="context">Db context</param>
        private void SeedNotificationTypes(NotificationSenderDbContext context)
        {
            if (!context.NotificationTypes.Any())
            {
                foreach (var e in Enum.GetValues(typeof(NotificationTypeEnum)))
                {
                    var notificationType = new NotificationType(e.ToString());
                    context.Add(notificationType);
                }

                context.SaveChanges();
            }
        }

        private void SeedMailSettings(NotificationSenderDbContext context)
        {
            if (!context.MailSettings.Any())
            {
                var mailSetting = new MailSetting(
                    "tm998576@gmail.com", 
                    PasswordEncryption.EncryptPassword("testMail(strong);"), 
                    "smtp.gmail.com", 587);

                context.Add(mailSetting);
                context.SaveChanges();
            }
        }
    }
}
