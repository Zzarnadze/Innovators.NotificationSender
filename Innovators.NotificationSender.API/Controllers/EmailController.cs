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
    /// Email Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Email")]
    [ApiController]
    [EnableCors("NotificationOrigins")]
    public class EmailController : BaseController
    {
        /// <summary>
        /// Notification Service Accessor
        /// </summary>
        private readonly INotificationService _notificationservice;

        /// <summary>
        /// Constructor for EmailController
        /// </summary>
        /// <param name="notificationService"></param>
        public EmailController(INotificationService notificationService)
        {
            _notificationservice = notificationService;
        }

        /// <summary>
        /// Sends Email
        /// </summary>
        /// <param name="model">Email</param>
        /// <returns>Status code</returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody]EmailDto model)
        {
            try
            {
                var response = await _notificationservice.SendEmail(model);
                if (response == ResultCodeEnum.Code200Success)
                    return Ok();

                return Error(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return Error(ResultCodeEnum.Code500InternalServerError);
            }
        }
    }
}