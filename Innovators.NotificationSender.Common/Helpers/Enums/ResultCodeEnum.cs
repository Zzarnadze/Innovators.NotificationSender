using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Helpers.Enums
{
    public enum ResultCodeEnum
    {
        Code200Success = 0,
        Code500InternalServerError = 1,
        Code204NoContent = 2,
        Code400BadRequest = 3,
        Code404NotFound = 4,
        Code409ResourceAlreadyExists = 5,
        Code500DatabasePersistError = 6,
        Code404MailSettingNotFound = 7
    }
}
