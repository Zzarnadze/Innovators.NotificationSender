using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Domain.DTOs;
using Innovators.NotificationSender.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innovators.NotificationSender.API.Controllers
{
    /// <summary>
    /// Sms Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Sms")]
    [ApiController]
    [EnableCors("NotificationOrigins")]
    public class SmsController : BaseController
    {
        /// <summary>
        /// Notification Service Accessor
        /// </summary>
        private readonly INotificationService _notificationservice;

        /// <summary>
        /// Notification Service Accessor
        /// </summary>
        public SmsController(INotificationService notificationService)
        {
            _notificationservice = notificationService;
        }

        /// <summary>
        /// Sends Sms
        /// </summary>
        /// <param name="model">Sms</param>
        /// <returns>Status code</returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody]SmsDto model)
        {
            try
            {
                var response = await _notificationservice.SendSms(model);
                if (response.Status == ResultCodeEnum.Code200Success)
                    return Ok();

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