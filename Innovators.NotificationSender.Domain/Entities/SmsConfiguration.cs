using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class SmsConfiguration :Base
    {
        [StringLength(500)]
        [Required]
        public string ServiceRequestUrl { get; set; }

        [Required]
        public int ServiceId { get; set; }
    }
}
