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
        Task<ResultWrapper<string>> SendEmail(EmailDto emailDto);

        Task<ResultWrapper<string>> SendSms(SmsDto smsDto);
    }
}
