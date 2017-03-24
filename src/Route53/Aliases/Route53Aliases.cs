#region Using Statements
    using System.Collections.Generic;

    using Cake.Core;
    using Cake.Core.IO;
    using Cake.Core.Annotations;

    using Amazon.Route53;
    using Amazon.Route53.Model;

    using Amazon.EC2;
    using Amazon.EC2.Model;
    using Amazon.Util;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Amazon Route53 aliases
    /// </summary>
    [CakeAliasCategory("AWS")]
    [CakeNamespaceImport("Amazon")]
    [CakeNamespaceImport("Amazon.Route53")]
    public static class Route53Aliases
    {
        private static IRoute53Manager CreateManager(this ICakeContext context)
        {
            return new Route53Manager(context.Environment, context.Log);
        }



        /// <summary>
        /// Create a new hosted zone. When you create a zone, its initial status is PENDING. This means that it is not yet available on all DNS servers.
        /// The status of the zone changes to INSYNC when the NS and SOA records are available on all Route 53 DNS servers.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="domain">The name of the domain</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string CreateHostedZone(this ICakeContext context, string domain, Route53Settings settings)
        {
            return context.CreateManager().CreateHostedZone(domain, "", null, settings);
        }

        /// <summary>
        /// Create a new hosted zone. When you create a zone, its initial status is PENDING. This means that it is not yet available on all DNS servers.
        /// The status of the zone changes to INSYNC when the NS and SOA records are available on all Route 53 DNS servers.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="domain">The name of the domain</param>
        /// <param name="vpc">The VPC that you want your hosted zone to be associated with. By providing this parameter, your newly created hosted cannot be resolved anywhere other than the given VPC.</param>
        /// <param name="region">The region of your VPC</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string CreateHostedZone(this ICakeContext context, string domain, string vpc, VPCRegion region, Route53Settings settings)
        {
            return context.CreateManager().CreateHostedZone(domain, vpc, region, settings);
        }

        /// <summary>
        /// Delete a hosted zone.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to delete</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static bool DeleteHostedZone(this ICakeContext context, string hostedZoneId, Route53Settings settings)
        {
            return context.CreateManager().DeleteHostedZone(hostedZoneId, settings);
        }



        /// <summary>
        /// Retrieve the hosted zone for a specific domain.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="domain">The name of the domain</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static HostedZone GetHostedZone(this ICakeContext context, string domain, Route53Settings settings)
        {
            return context.CreateManager().GetHostedZone(domain, settings);
        }

        /// <summary>
        /// Retrieve a list of your hosted zones.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static IList<HostedZone> GetHostedZones(this ICakeContext context, Route53Settings settings)
        {
            return context.CreateManager().GetHostedZones(settings);
        }



        /// <summary>
        /// Create or change a DNS record pointing to the current instance
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string CreateResourceRecordSet(this ICakeContext context, string hostedZoneId, string name, RRType type, Route53Settings settings)
        {
            return context.CreateManager().CreateResourceRecordSet(hostedZoneId, name, type, EC2InstanceMetadata.PrivateIpAddress, 300, settings);
        }

        /// <summary>
        /// Create or change a DNS record for a hosted zone.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="value">The value of the record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string CreateResourceRecordSet(this ICakeContext context, string hostedZoneId, string name, RRType type, string value, Route53Settings settings)
        {
            return context.CreateManager().CreateResourceRecordSet(hostedZoneId, name, type, value, 300, settings);
        }

        /// <summary>
        /// Create or change a DNS record for a hosted zone.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="value">The value of the record set.</param>
        /// <param name="ttl">The time to live of the record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string CreateResourceRecordSet(this ICakeContext context, string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings)
        {
            return context.CreateManager().CreateResourceRecordSet(hostedZoneId, name, type, value, ttl, settings);
        }

        /// <summary>
        /// Delete a DNS record for a hosted zone.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="value">The value of the record set.</param>
        /// <param name="ttl">The time to live of the record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static string DeleteResourceRecordSet(this ICakeContext context, string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings)
        {
            return context.CreateManager().DeleteResourceRecordSet(hostedZoneId, name, type, value, ttl, settings);
        }

        /// <summary>
        /// To retrieve a list of record sets for a particular hosted zone.
        /// </summary>
        /// <param name="context">The cake context.</param>
        /// <param name="hostedZoneId">The ID of the hosted zone whos record sets you want to list</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        [CakeMethodAlias]
        [CakeAliasCategory("Route53")]
        public static IList<ResourceRecordSet> GetResourceRecordSets(this ICakeContext context, string hostedZoneId, Route53Settings settings)
        {
            return context.CreateManager().GetResourceRecordSets(hostedZoneId, settings);
        }
    }
}
