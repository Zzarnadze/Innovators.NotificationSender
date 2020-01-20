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
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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



                var body = new BodyBuilder
                {
                    HtmlBody = email.Body
                };

                foreach (var attachment in email.Attachments)
                {
                    var attachmentStream = new MemoryStream(attachment.AttachmentData);
                    body.Attachments.Add(attachment.AttachmentName, attachmentStream);
                }
                message.From.Add(new MailboxAddress(mailconfiguration.Email, email.SenderDisplay));
                message.To.Add(new MailboxAddress(email.Receiver));
                message.Subject = email.Subject;
                message.Body = body.ToMessageBody();


                using (var client = new MailKit.Net.Smtp.SmtpClient())
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
                notifications.CreatedByCustomerId =99;

             var xx=   _context.Add(notifications);


                const string accountSid = "ACee8ca879e0568350c9da34d78ce561e1";
                const string authToken = "db6e053a339b9670d7e2b3557fad7dba";

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("+995599212227");
                var message = MessageResource.Create(
                    to: to,
                    from: new PhoneNumber("+12512502319"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).
                    body: $"Hello {+995599212227} !! Welcome to Asp.Net Core With Twilio SMS API !!");

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



