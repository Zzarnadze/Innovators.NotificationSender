using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
   public class MailTemplate :Base
    {


        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public string Template { get; set; }


    }
}
