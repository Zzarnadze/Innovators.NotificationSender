using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innovators.NotificationSender.Domain.Entities
{
  public  class MailSetting : Base
    {
        #region Constructors
        public MailSetting() { }

        public MailSetting(
            string email,
            string password,
            string host,
            int port)
        {
            Email = email;
            Host = host;
            Port = port;
            Password = password;
        }
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; } 
        #endregion
    }
}
