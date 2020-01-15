using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Domain.DTOs;
using Innovators.NotificationSender.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innovators.NotificationSender.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Notification")]
    [ApiController]
    [EnableCors("NotificationOrigins")]

    public class NotificationController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly INotificationService _notificationservice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationService"></param>
        public NotificationController(INotificationService notificationService)
        {
            _notificationservice = notificationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody]EmailDto model)
        {

           try
            {
                var response = await _notificationservice.SendEmail(model);
                if (response.Status == ResultCodeEnum.Code200Success)
                    return CreatedAtAction("SendMail",  response.Value);

               return Error(response.Status);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return Error(ResultCodeEnum.Code500InternalServerError);
            }
        }


        [ProducesResponseType(typeof(string), 200)]
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendSms([FromBody]SmsDto model)
        {

            try
            {
                var response = await _notificationservice.SendSms(model);
                if (response.Status == ResultCodeEnum.Code200Success)
                    return CreatedAtAction("SendSms", response.Value);

                return Error(response.Status);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return Error(ResultCodeEnum.Code500InternalServerError);
            }
        }

    }
}