using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innovators.NotificationSender.Common.Helpers.Enums;
using Innovators.NotificationSender.Common.Helpers.Utilities.Resources;
using Innovators.NotificationSender.Common.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Innovators.NotificationSender.API.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCode"></param>
        /// <returns></returns>
        protected IActionResult Error (ResultCodeEnum resultCode)
        {
            try
            {
                var message = ResourceUtils.GetresponseResourceString(resultCode.ToString());
                var code = Convert.ToInt32(resultCode.ToString().Substring(4, 3));
                Log.Error(message);
                return StatusCode(code, message);
            }
            catch(Exception ex)
            {
                Log.Error(ex, ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ResourceUtils.GetresponseResourceString(ResultCodeEnum.Code500InternalServerError.ToString()));
            }
        }
    }
}