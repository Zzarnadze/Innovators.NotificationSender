using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class Notification : Base
    {
        public string Sender { get; set; }
        public string SenderDisplay { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ProviderName { get; set; }
        public int ServiceId { get; set; }
        public int NotificationTypeId { get; set; }
        public bool IsBodyHtml { get; set; }
        public string MessageId { get; set; }
        public int? TemplateId { get; set; }
        public string TemplateItems { get; set; }
        public int NotificationStatusId { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
