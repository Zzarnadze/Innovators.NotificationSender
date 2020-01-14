using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
  public  class MailConfiguration :Base
    {
        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        [Required]
        [StringLength(300)]
        public string UserName { get; set; }

        [Required]
        [StringLength(300)]
        public string Password { get; set; }

        [Required]
        [StringLength(300)]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }


    }
}
