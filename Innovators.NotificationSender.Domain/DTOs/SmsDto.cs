using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Domain.DTOs
{
    public class SmsDto
    {
        public string Body { get; set; }

        public string Reciver { get; set; }

        public bool IsUnicode { get; set; }

    }
}
