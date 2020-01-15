using AutoMapper;
using Innovators.NotificationSender.Common.HelperModel;
using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Common.Helpers.Models;
using Innovators.NotificationSender.Common.Helpers.Utilities;
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
        private readonly NotificationSenderDbCondetxt _context;
        private readonly IMapper _mapper;

        public NotificationService(
            NotificationSenderDbCondetxt condetxt,
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
        public async Task<ResultWrapper<string>> SendEmail(EmailDto email)
        {
            try
            {
                var mailconfiguration = await _context.MailConfigurations.FirstOrDefaultAsync(x => x.IsActive == true &&x.IsDeleted==false);

                if (mailconfiguration == null)
                {
                    return new ResultWrapper<string>
                    {
                        Status = ResultCodeEnum.Code404NotFound
                    };
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailconfiguration.Email, email.SenderDisplay));
                message.To.Add(new MailboxAddress(email.Receiver));
                message.Subject = email.Subject;
                message.Body = new TextPart("html")
                {
                    Text = email.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(mailconfiguration.Host, mailconfiguration.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(mailconfiguration.Email, mailconfiguration.Password);
                    client.Send(message);
                    client.Disconnect(false);

                }



                var notifications = _mapper.Map<Notification>(email);

                notifications.NotificationTypeId = (int)NotificationTypeEnum.Email;
                notifications.ServiceId = 1;
                notifications.Sender = mailconfiguration.Email;
                notifications.SenderDisplay = email.SenderDisplay;
                notifications.CreatedByCustomerId = 1;
                _context.Add(notifications);
                _context.SaveChanges();


                return new ResultWrapper<string>
                {
                    Status = ResultCodeEnum.Code200Success,
                    Value = null

                };
            }
            catch (Exception ex)
            {
                return new ResultWrapper<string>
                {
                    Status = ResultCodeEnum.Code404NotFound
                };
            }
        }


        public async Task<ResultWrapper<string>> SendSms(SmsDto request)
        {
            try
            {
                var smsconfiguration = await _context.SmsConfigurations.FirstOrDefaultAsync(x => x.IsActive == true && x.IsDeleted == false);
                if (smsconfiguration == null)
                {
                    return new ResultWrapper<string>
                    {
                        Status = ResultCodeEnum.Code404NotFound
                    };
                }
                var notifications = _mapper.Map<Notification>(request);

                notifications.NotificationTypeId = (int)NotificationTypeEnum.Sms;
                notifications.ProviderName = "Magti";
                notifications.ServiceId = smsconfiguration.ServiceId;
                notifications.Sender = "client";
                notifications.SenderDisplay = "Test";
                notifications.CreatedByCustomerId =1;

                _context.Add(notifications);
                

                var coding = request.IsUnicode ? (int)CharacterEncodingEnum.Unicode : (int)CharacterEncodingEnum.Default;
                var mainText = request.Body;
                var url = string.Format(smsconfiguration.ServiceRequestUrl, smsconfiguration.ServiceId, SmsUtilities.FixReceiveNumber(request.Reciver), mainText, coding);
                var statusObject = new SmsSendStatus();

                HttpResponseMessage result = new HttpResponseMessage();

                using (HttpClient client = new HttpClient())
                {
                    result = client.GetAsync(url).Result;
                }

                if (result != null && result.IsSuccessStatusCode)
                {
                    var smsSendResult = result.Content.ReadAsStringAsync().Result;
                    statusObject = SmsUtilities.ParseSmsStatusCode(smsSendResult);
                }
                else
                {
                    notifications.NotificationStatusId = (int)SmsSendStatusCodeEnum.ServerError;
                    _context.Update(notifications);

                    return new ResultWrapper<string>
                    {
                        Status = ResultCodeEnum.Code200Success,
                        Value = null

                    };
                }

                if (statusObject.Status == SmsSendStatusCodeEnum.Success)
                    notifications.MessageId = statusObject.MessageId;

                notifications.NotificationStatusId = (int)statusObject.Status;
                _context.Update(notifications);

                return new ResultWrapper<string>
                {
                    Status = ResultCodeEnum.Code200Success,
                    Value = null

                };
            }
            catch (Exception ex)
            {
                return new ResultWrapper<string>
                {
                    Status = ResultCodeEnum.Code404NotFound
                };
            }
        }


        
    }

}



