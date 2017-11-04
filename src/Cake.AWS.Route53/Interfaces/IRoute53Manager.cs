#region Using Statements
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Amazon.Route53;
using Amazon.Route53.Model;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Provides a client for making API requests to Amazon Route 53.
    /// </summary>
    public interface IRoute53Manager
    {
        #region Methods
        /// <summary>
        /// Create a new hosted zone. When you create a zone, its initial status is PENDING. This means that it is not yet available on all DNS servers.
        /// The status of the zone changes to INSYNC when the NS and SOA records are available on all Route 53 DNS servers.
        /// </summary>
        /// <param name="domain">The name of the domain</param>
        /// <param name="vpc">The VPC that you want your hosted zone to be associated with. By providing this parameter, your newly created hosted cannot be resolved anywhere other than the given VPC.</param>
        /// <param name="region">The region of your VPC</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<string> CreateHostedZone(string domain, string vpc, VPCRegion region, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete a hosted zone.
        /// </summary>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to delete</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<bool> DeleteHostedZone(string hostedZoneId, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));



        /// <summary>
        /// Retrieve the hosted zone for a specific domain.
        /// </summary>
        /// <param name="domain">The name of the domain</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<HostedZone> GetHostedZone(string domain, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieve a list of your hosted zones.
        /// </summary>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<IList<HostedZone>> GetHostedZones(Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));



        /// <summary>
        /// Create or change a DNS record for a hosted zone.
        /// </summary>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="value">The value of the record set.</param>
        /// <param name="ttl">The time to live of the record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<string> CreateResourceRecordSet(string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete a DNS record for a hosted zone.
        /// </summary>
        /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
        /// <param name="name">The name of the DNS record set.</param>
        /// <param name="type">The type of the DNS record set.</param>
        /// <param name="value">The value of the record set.</param>
        /// <param name="ttl">The time to live of the record set.</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<string> DeleteResourceRecordSet(string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// To retrieve a list of record sets for a particular hosted zone.
        /// </summary>
        /// <param name="hostedZoneId">The ID of the hosted zone whos record sets you want to list</param>
        /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        Task<IList<ResourceRecordSet>> GetResourceRecordSets(string hostedZoneId, Route53Settings settings, CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}
