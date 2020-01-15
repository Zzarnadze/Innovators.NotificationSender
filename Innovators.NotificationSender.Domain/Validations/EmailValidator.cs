using FluentValidation;
using Innovators.NotificationSender.Common.Resources;
using Innovators.NotificationSender.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Domain.Validations
{
    public class EmailValidator : AbstractValidator<EmailDto>
    {
        public EmailValidator()
        {
            RuleFor(m => m.Body).NotEmpty().WithMessage(ValidationMessageResources.BodyIsRequired);
            RuleFor(m => m.Receiver).NotEmpty().WithMessage(ValidationMessageResources.ReceiverIsRequired);
            RuleFor(m => m.Subject).NotEmpty().WithMessage(ValidationMessageResources.SubjectIsRequired);
        }
    }
}
