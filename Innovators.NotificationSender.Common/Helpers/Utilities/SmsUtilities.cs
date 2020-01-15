using Innovators.NotificationSender.Common.HelperModel;
using Innovators.NotificationSender.Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Helpers.Utilities
{
    public class SmsUtilities
    {

        public static SmsSendStatus ParseSmsStatusCode (string rawCode)
        {
            var ParseCode = (int)Char.GetNumericValue(rawCode[3]);
            var messageId = ParseCode == 0 ? rawCode.Substring(7) : "";

            return new SmsSendStatus
            {
                Status = (SmsSendStatusCodeEnum)ParseCode,
                MessageId = messageId
            };
        }

        public static string FixReceiveNumber (string rawNumber)
        {
            if (rawNumber.Length == 9)
            {
                rawNumber = "+995" + rawNumber;
            }
            return rawNumber;
        }
    }
}
