using Innovators.NotificationSender.Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.HelperModel
{
    public class SmsSendStatus
    {
        public SmsSendStatusCodeEnum Status { get; set; }
        public string MessageId { get; set; }
    }
}
