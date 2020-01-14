using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class NotificationType : Base
    {


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
