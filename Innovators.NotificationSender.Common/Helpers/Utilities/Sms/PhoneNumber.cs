using Innovators.NotificationSender.Common.HelperModel;
using Innovators.NotificationSender.Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Helpers.Utilities.Sms
{
    public class PhoneNumber
    {
        /// <summary>
        /// Parses sms status code returned from service provider
        /// </summary>
        /// <param name="rawCode">Raw code</param>
        /// <returns>Parsed code</returns>
        public static SmsSendStatus ParseSmsStatusCode (string rawCode)
        {
            var ParseCode = (int)char.GetNumericValue(rawCode[3]);
            var messageId = ParseCode == 0 ? rawCode.Substring(7) : "";

            return new SmsSendStatus
            {
                Status = (SmsSendStatusCodeEnum)ParseCode,
                MessageId = messageId
            };
        }

        /// <summary>
        /// Fixes receiver number
        /// </summary>
        /// <param name="rawNumber">Raw number</param>
        /// <returns>Fixed number</returns>
        public static string FixReceiverNumber (string rawNumber)
        {
            if (rawNumber.Length == 9)
            {
                rawNumber = "+995" + rawNumber;
            }
            return rawNumber;
        }
    }
}
