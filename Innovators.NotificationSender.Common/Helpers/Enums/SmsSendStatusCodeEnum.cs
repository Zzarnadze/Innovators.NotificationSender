using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Helpers.Enums
{
    public enum SmsSendStatusCodeEnum
    {
        Success = 0,
        InternalError = 1,
        InvalidRequest = 3,
        InvalidQuery = 4,
        EmptyMessage = 5,
        PrefixError = 6,
        MsisdnError = 7,
        ServerError = 8
    }
}
