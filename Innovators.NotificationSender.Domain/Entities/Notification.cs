using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class Notification : Base
    {
        public Notification() { }
        public Notification(
            string sender,
            string senderDisplay,
            string receiver,
            string subject,
            string body,
            string providerName,
            int serviceId,
            int notificationTypeId,
            string messageId,
            int templateId,
            string templateItems,
            int subTemplateId,
            string subTemplateItems,
             int notificationStatusId)
        {
            Sender = sender;
            SenderDisplay = senderDisplay;
            Receiver = receiver;
            Subject = subject;
            Body = body;
            ProviderName = providerName;
            ServiceId = serviceId;
            NotificationTypeId = NotificationTypeId;
            MessageId = messageId;
            TemplateId = templateId;
            TemplateItems = templateItems;
            SubTemplateItems = subTemplateItems;
            NotificationStatusId = notificationStatusId;
        }

        [Required]
        [StringLength(254)]
        public string Sender { get; set; }

        [StringLength(50)]
        public string SenderDisplay { get; set; }

        [Required]
        [StringLength(254)]
        public string Receiver { get; set; }

        [StringLength(300)]
        public string Subject { get; set; }

        [StringLength(1000)]
        public string Body { get; set; }

        [StringLength(300)]
        public string ProviderName { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int NotificationTypeId { get; set; }


        [Required]
        public bool IsBodyHtml { get; set; }

        [StringLength(10)]
        public string MessageId { get; set; }

        public int? TemplateId { get; set; }

        public string TemplateItems { get; set; }

        public int? SubTemplateId { get; set; }
        public string SubTemplateItems { get; set; }


        public  virtual User User { get; set; }

        public int NotificationStatusId { get; set; }
    }
}
