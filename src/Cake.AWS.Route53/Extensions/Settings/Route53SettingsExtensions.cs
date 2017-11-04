﻿#region Using Statements
using System;

using Amazon;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Contains extension methods for <see cref="Route53Settings" />.
    /// </summary>
    public static class Route53SettingsExtensions
    {
        /// <summary>
        /// Specifies the AWS Access Key to use as credentials.
        /// </summary>
        /// <param name="settings">The Route53 settings.</param>
        /// <param name="key">The AWS Access Key</param>
        /// <returns>The same <see cref="Route53Settings"/> instance so that multiple calls can be chained.</returns>
        public static Route53Settings SetAccessKey(this Route53Settings settings, string key)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            settings.AccessKey = key;
            return settings;
        }

        /// <summary>
        /// Specifies the AWS Secret Key to use as credentials.
        /// </summary>
        /// <param name="settings">The Route53 settings.</param>
        /// <param name="key">The AWS Secret Key</param>
        /// <returns>The same <see cref="Route53Settings"/> instance so that multiple calls can be chained.</returns>
        public static Route53Settings SetSecretKey(this Route53Settings settings, string key)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            settings.SecretKey = key;
            return settings;
        }



        /// <summary>
        /// Specifies the endpoints available to AWS clients.
        /// </summary>
        /// <param name="settings">The Route53 settings.</param>
        /// <param name="region">The endpoints available to AWS clients.</param>
        /// <returns>The same <see cref="Route53Settings"/> instance so that multiple calls can be chained.</returns>
        public static Route53Settings SetRegion(this Route53Settings settings, string region)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            settings.Region = RegionEndpoint.GetBySystemName(region);
            return settings;
        }

        /// <summary>
        /// Specifies the endpoints available to AWS clients.
        /// </summary>
        /// <param name="settings">The Route53 settings.</param>
        /// <param name="region">The endpoints available to AWS clients.</param>
        /// <returns>The same <see cref="Route53Settings"/> instance so that multiple calls can be chained.</returns>
        public static Route53Settings SetRegion(this Route53Settings settings, RegionEndpoint region)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            settings.Region = region;
            return settings;
        }
    }
}
