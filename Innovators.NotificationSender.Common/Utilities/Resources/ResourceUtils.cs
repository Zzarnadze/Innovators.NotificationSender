using System;
using System.Collections.Generic;
using System.Text;

namespace Innovators.NotificationSender.Common.Resources
{
    public class ResourceUtils
    {

        public static string GetresponseResourceString (string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            try
            {
                return ResponseResources.ResourceManager.GetString(key);
            }
            catch
            {
                return key;
            }
        }
    }
}
