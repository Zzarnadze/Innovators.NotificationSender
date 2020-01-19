using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
   public class MailTemplate : Base
    {
        public string Name { get; set; }
        public string Template { get; set; }
    }
}
