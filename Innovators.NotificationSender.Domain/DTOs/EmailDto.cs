using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Domain.DTOs
{
   public class EmailDto
    {
        public string Body { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string SenderDisplay { get; set; }
     //   public IEnumerable<AttachmentDto> Attachments { get; set; }
        public bool IsBodyHtml { get; set; } = false;
    }

    public class AttachmentDto
    {
        public byte[] AttachmentData { get; set; }
        public string AttachmentName { get; set; }
    }
}
