using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class SmsSetting : Base
    {
        public string ServiceRequestUrl { get; set; }
        public int ServiceId { get; set; }
    }
}
