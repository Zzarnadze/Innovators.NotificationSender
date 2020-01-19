using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
    public class NotificationType : Base
    {
        #region Constructors
        public NotificationType() { }

        public NotificationType(string name)
        {
            Name = name;
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; } 
        #endregion
    }
}
