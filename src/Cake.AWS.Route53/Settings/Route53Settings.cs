﻿#region Using Statements
using Amazon;
using Amazon.Runtime;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// The settings to use with requests to Amazon S3
    /// </summary>
    public class Route53Settings
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Route53Settings" /> class.
        /// </summary>
        public Route53Settings()
        {
            Region = RegionEndpoint.EUWest1;
        }
        #endregion





        #region Properties
        /// <summary>
        /// The AWS Access Key ID
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// The AWS Secret Access Key.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// The AWS Session Token, if using temporary credentials.
        /// </summary>
        public string SessionToken { get; set; }

        internal AWSCredentials Credentials { get; set; }



        /// <summary>
        /// The endpoints available to AWS clients.
        /// </summary>
        public RegionEndpoint Region { get; set; }
        #endregion
    }
}
