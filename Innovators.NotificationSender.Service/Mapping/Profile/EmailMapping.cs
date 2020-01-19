using Innovators.NotificationSender.Service.Mappings.Profiles;
using Innovators.NotificationSender.Domain.DTOs;
using Innovators.NotificationSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Service.Mapping.Profile
{
    class EmailMapping : BaseMappingProfile
    {
        public EmailMapping()
        {
            CreateMap<Notification, EmailDto>();
            CreateMap<EmailDto, Notification>();
        }
    }
}
