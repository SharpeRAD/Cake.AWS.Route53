#region Using Statements
using System;

using Cake.Core;

using Amazon;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Contains extension methods for <see cref="ICakeContext" />.
    /// </summary>
    public static class CakeContextExtensions
    {
        /// <summary>
        /// Helper method to get the AWS Credentials from environment variables
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <returns>A new <see cref="Route53Settings"/> instance to be used in calls to the <see cref="IRoute53Manager"/>.</returns>
        public static Route53Settings CreateDownloadSettings(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.Environment.CreateRoute53Settings();
        }
    }
}
