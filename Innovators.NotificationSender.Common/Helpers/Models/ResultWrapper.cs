using Innovators.NotificationSender.Common.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Helpers.Models
{
   public class ResultWrapper<T>
    {
        public ResultCodeEnum Status { get; set; }
        public T Value { get; set; }
    }
}
