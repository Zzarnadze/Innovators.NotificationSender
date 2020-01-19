using AutoMapper;
using Innovators.NotificationSender.Common.HelperModel;
using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Common.Helpers.Models;
using Innovators.NotificationSender.Common.Helpers.Utilities.Encryption;
using Innovators.NotificationSender.Common.Helpers.Utilities.Sms;
using Innovators.NotificationSender.Domain.DTOs;
using Innovators.NotificationSender.Domain.Entities;
using Innovators.NotificationSender.Domain.Enums;
using Innovators.NotificationSender.Persistence.Context;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Innovators.NotificationSender.Service.Services
{
    public class NotificationService : INotificationService
    {
        private const string CryptoKey = "123";

        private readonly NotificationSenderDbContext _context;
        private readonly IMapper _mapper;

        public NotificationService(
            NotificationSenderDbContext condetxt,
            IMapper mapper)
        {
            _context = condetxt;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ResultCodeEnum> SendEmail(EmailDto email)
        {
            try
            {
                var mailSetting = await _context.MailSettings.FirstOrDefaultAsync(x => x.IsActive == true &&x.IsDeleted == false);

                if (mailSetting == null)
                    return ResultCodeEnum.Code404NotFound;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailSetting.Email, email.SenderDisplay));
                message.To.Add(new MailboxAddress(email.Receiver));
                message.Subject = email.Subject;
                message.Body = new TextPart("html")
                {
                    Text = email.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(mailSetting.Host, mailSetting.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(mailSetting.Email, PasswordEncryption.DecryptPassword(CryptoKey, mailSetting.Password));
                    client.Send(message);
                    client.Disconnect(false);
                }



                var notifications = _mapper.Map<Notification>(email);

                notifications.NotificationTypeId = (int)NotificationTypeEnum.Email;
                notifications.ServiceId = 1;
                notifications.Sender = mailSetting.Email;
                notifications.SenderDisplay = email.SenderDisplay;
                notifications.CreatedByCustomerId = 1;
                _context.Add(notifications);
                _context.SaveChanges();


                return ResultCodeEnum.Code200Success;
            }
            catch (Exception ex)
            {
                return ResultCodeEnum.Code500InternalServerError;
            }
        }


        public async Task<ResultCodeEnum> SendSms(SmsDto request)
        {
            try
            {
                var smsSetting = await _context.SmsSettings.FirstOrDefaultAsync(x => x.IsActive == true && x.IsDeleted == false);
                if (smsSetting == null)
                {
                    return ResultCodeEnum.Code404NotFound;
                }
                var notifications = _mapper.Map<Notification>(request);

                notifications.NotificationTypeId = (int)NotificationTypeEnum.Sms;
                notifications.ProviderName = "Magti";
                notifications.ServiceId = smsSetting.ServiceId;
                notifications.Sender = "client";
                notifications.SenderDisplay = "Test";
                notifications.CreatedByCustomerId =1;

                _context.Add(notifications);
                

                var coding = request.IsUnicode ? (int)CharacterEncodingEnum.Unicode : (int)CharacterEncodingEnum.Default;
                var mainText = request.Body;
                var url = string.Format(smsSetting.ServiceRequestUrl, smsSetting.ServiceId, PhoneNumber.FixReceiverNumber(request.Reciver), mainText, coding);
                var statusObject = new SmsSendStatus();

                HttpResponseMessage result = new HttpResponseMessage();

                using (HttpClient client = new HttpClient())
                {
                    result = client.GetAsync(url).Result;
                }

                if (result != null && result.IsSuccessStatusCode)
                {
                    var smsSendResult = result.Content.ReadAsStringAsync().Result;
                    statusObject = PhoneNumber.ParseSmsStatusCode(smsSendResult);
                }
                else
                {
                    notifications.NotificationStatusId = (int)SmsSendStatusCodeEnum.ServerError;
                    _context.Update(notifications);

                    return ResultCodeEnum.Code200Success;
                }

                if (statusObject.Status == SmsSendStatusCodeEnum.Success)
                    notifications.MessageId = statusObject.MessageId;

                notifications.NotificationStatusId = (int)statusObject.Status;
                _context.Update(notifications);

                return ResultCodeEnum.Code200Success;
            }
            catch (Exception ex)
            {
                return ResultCodeEnum.Code500InternalServerError;
            }
        }


        
    }

}



