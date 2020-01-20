using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Common.Helpers.Models;
using Innovators.NotificationSender.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Innovators.NotificationSender.Service.Services
{
    public interface INotificationService
    {
        Task<ResultCodeEnum> SendEmail(EmailDto emailDto);

        Task<ResultCodeEnum> SendSms(SmsDto smsDto);
    }
}
