using Innovators.NotificationSender.Service.Mappings.Profiles;
using Innovators.NotificationSender.Domain.DTOs;
using Innovators.NotificationSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Service.Mapping.Profile
{
    class SmsMapping : BaseMappingProfile
    {
        public SmsMapping()
        {
            CreateMap<Notification, SmsDto>();
            CreateMap<SmsDto, Notification>();
        }
    }
}
