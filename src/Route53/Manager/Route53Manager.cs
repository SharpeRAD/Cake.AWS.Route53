#region Using Statements
    using System;
    using System.Net;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;

    using Cake.Core;
    using Cake.Core.IO;
    using Cake.Core.Diagnostics;

    using Amazon.Route53;
    using Amazon.Route53.Model;
#endregion



namespace Cake.AWS.Route53
{
    /// <summary>
    /// Provides a client for making API requests to Amazon Route 53.
    /// </summary>
    public class Route53Manager : IRoute53Manager
    {
        #region Fields (2)
            private readonly ICakeEnvironment _Environment;
            private readonly ICakeLog _Log;
        #endregion





        #region Constructor (1)
            /// <summary>
            /// Initializes a new instance of the <see cref="Route53Manager" /> class.
            /// </summary>
            /// <param name="environment">The environment.</param>
            /// <param name="log">The log.</param>
            public Route53Manager(ICakeEnvironment environment, ICakeLog log)
            {
                if (environment == null)
                {
                    throw new ArgumentNullException("environment");
                }
                if (log == null)
                {
                    throw new ArgumentNullException("log");
                }

                _Environment = environment;
                _Log = log;
            }
        #endregion





        #region Functions (9)
            //Helpers
            private AmazonRoute53Client GetClient(Route53Settings settings)
            {
                if (settings == null)
                {
                    throw new ArgumentNullException("settings");
                }

                if (settings.Region == null)
                {
                    throw new ArgumentNullException("settings.Region");
                }

                if (settings.Credentials == null)
                {
                    if (String.IsNullOrEmpty(settings.AccessKey))
                    {
                        throw new ArgumentNullException("settings.AccessKey");
                    }
                    if (String.IsNullOrEmpty(settings.SecretKey))
                    {
                        throw new ArgumentNullException("settings.SecretKey");
                    }

                    return new AmazonRoute53Client(settings.AccessKey, settings.SecretKey, settings.Region);
                }
                else
                {
                    return new AmazonRoute53Client(settings.Credentials, settings.Region);
                }
            }

            private bool WaitForChange(AmazonRoute53Client client, string id, int interval = 10000, int maxIterations = 60)
            {
                bool pending = true;
                bool timeout = (maxIterations > 0);
                int count = 0;

                while (pending && !timeout)
                {
                    //Check Status
                    GetChangeResponse response = client.GetChange(new GetChangeRequest(id));

                    if ((response != null) && (response.HttpStatusCode == HttpStatusCode.OK))
                    {
                        if (ChangeStatus.INSYNC == response.ChangeInfo.Status)
                        {
                            pending = false;
                        }
                    }
                    else
                    {
                        timeout = true;
                    }



                    //Sleep
                    if (count < maxIterations)
                    {
                        Console.WriteLine("Change is pending...");
                        Thread.Sleep(interval);
                    }
                    else
                    {
                        timeout = true;
                    }

                    count++;
                }

                return timeout;
            }



            /// <summary>
            /// Create a new hosted zone. When you create a zone, its initial status is PENDING. This means that it is not yet available on all DNS servers.
            /// The status of the zone changes to INSYNC when the NS and SOA records are available on all Route 53 DNS servers.
            /// </summary>
            /// <param name="domain">The name of the domain</param>
            /// <param name="vpc">The VPC that you want your hosted zone to be associated with. By providing this parameter, your newly created hosted cannot be resolved anywhere other than the given VPC.</param>
            /// <param name="region">The region of your VPC</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
            public string CreateHostedZone(string domain, string vpc, VPCRegion region, Route53Settings settings)
            {
                if (String.IsNullOrEmpty(domain))
                {
                    throw new ArgumentNullException("domain");
                }



                CreateHostedZoneRequest request = new CreateHostedZoneRequest(domain, "");

                if (!String.IsNullOrEmpty(vpc))
                {
                    request.VPC = new VPC()
                    {
                        VPCId = vpc,
                        VPCRegion = region
                    };
                }

                AmazonRoute53Client client = this.GetClient(settings);
                CreateHostedZoneResponse response = client.CreateHostedZone(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    this.WaitForChange(client, response.ChangeInfo.Id, 10000, 60);

                    _Log.Verbose("Created hosted zone");
                    return response.HostedZone.Id;
                }
                else
                {
                    _Log.Error("Could not create hosted zone");
                    return "";
                }
            }

            /// <summary>
            /// Delete a hosted zone.
            /// </summary>
            /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to delete</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
            public bool DeleteHostedZone(string hostedZoneId, Route53Settings settings)
            {
                if (String.IsNullOrEmpty(hostedZoneId))
                {
                    throw new ArgumentNullException("hostedZoneId");
                }



                DeleteHostedZoneRequest request = new DeleteHostedZoneRequest(hostedZoneId);

                AmazonRoute53Client client = this.GetClient(settings);
                DeleteHostedZoneResponse response = client.DeleteHostedZone(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    this.WaitForChange(client, response.ChangeInfo.Id, 10000, 60);

                    _Log.Verbose("deleted hosted zone");
                    return true;
                }
                else
                {
                    _Log.Error("Could not delete hosted zone");
                    return false;
                }
            }



            /// <summary>
            /// Retrieve the hosted zone for a specific domain.
            /// </summary>
            /// <param name="domain">The name of the domain</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
            public HostedZone GetHostedZone(string domain, Route53Settings settings)
            {
                if (String.IsNullOrEmpty(domain))
                {
                    throw new ArgumentNullException("domain");
                }



                IList<HostedZone> zones = this.GetHostedZones(settings);
                HostedZone zone = null;

                if (zones != null)
                {
                    zone = zones.FirstOrDefault(z => z.Name == domain);
                }

                if (zone == null)
                {
                    _Log.Error("Could not find a hosted zone with the domain {0}", domain);
                }

                return zone;
            }

            /// <summary>
            /// Retrieve a list of your hosted zones.
            /// </summary>
            /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
            public IList<HostedZone> GetHostedZones(Route53Settings settings)
            {
                ListHostedZonesRequest request = new ListHostedZonesRequest();

                AmazonRoute53Client client = this.GetClient(settings);
                ListHostedZonesResponse response = client.ListHostedZones(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    _Log.Verbose("Listing hosted zones");
                    return response.HostedZones;
                }
                else
                {
                    _Log.Error("Could not list hosted zones");
                    return null;
                }
            }



            /// <summary>
            /// Create or change a DNS record for a hosted zone.
            /// </summary>
            /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
            /// <param name="name">The name of the DNS record set.</param>
            /// <param name="type">The type of the DNS record set.</param>
            /// <param name="value">The value of the record set.</param>
            /// <param name="ttl">The time to live of the record set.</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
            public string CreateResourceRecordSet(string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings)
            {
                var recordSet = new ResourceRecordSet()
                {
                    Name = name,
                    TTL = ttl,
                    Type = type,
                    ResourceRecords = new List<ResourceRecord>
                    {
                        new ResourceRecord { Value = value }
                    }
                };

                var change1 = new Change()
                {
                    ResourceRecordSet = recordSet,
                    Action = ChangeAction.UPSERT
                };

                var changeBatch = new ChangeBatch()
                {
                    Changes = new List<Change> { change1 }
                };

                var recordsetRequest = new ChangeResourceRecordSetsRequest()
                {
                    HostedZoneId = hostedZoneId,
                    ChangeBatch = changeBatch
                };



                AmazonRoute53Client client = this.GetClient(settings);
                ChangeResourceRecordSetsResponse response = client.ChangeResourceRecordSets(recordsetRequest);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    this.WaitForChange(client, response.ChangeInfo.Id, 10000, 60);

                    _Log.Verbose("Updated record set");
                    return response.ChangeInfo.Id;
                }
                else
                {
                    _Log.Error("Could not change resource records");
                    return "";
                }
            }

            /// <summary>
            /// Delete a DNS record for a hosted zone.
            /// </summary>
            /// <param name="hostedZoneId">The ID of the hosted zone that contains the resource record sets that you want to change</param>
            /// <param name="name">The name of the DNS record set.</param>
            /// <param name="type">The type of the DNS record set.</param>
            /// <param name="value">The value of the record set.</param>
            /// <param name="ttl">The time to live of the record set.</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to upload to Amazon S3.</param>
            public string DeleteResourceRecordSet(string hostedZoneId, string name, RRType type, string value, long ttl, Route53Settings settings)
            {
                var recordSet = new ResourceRecordSet()
                {
                    Name = name,
                    Type = type,
                    TTL = ttl,
                    ResourceRecords = new List<ResourceRecord> { new ResourceRecord(value) }
                };

                var change1 = new Change()
                {
                    ResourceRecordSet = recordSet,
                    Action = ChangeAction.DELETE
                };

                var changeBatch = new ChangeBatch()
                {
                    Changes = new List<Change> { change1 }
                };

                var recordsetRequest = new ChangeResourceRecordSetsRequest()
                {
                    HostedZoneId = hostedZoneId,
                    ChangeBatch = changeBatch
                };



                AmazonRoute53Client client = this.GetClient(settings);
                ChangeResourceRecordSetsResponse response = client.ChangeResourceRecordSets(recordsetRequest);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    this.WaitForChange(client, response.ChangeInfo.Id, 10000, 60);

                    _Log.Verbose("Updated record set");
                    return response.ChangeInfo.Id;
                }
                else
                {
                    _Log.Error("Could not change resource records");
                    return "";
                }
            }

            /// <summary>
            /// To retrieve a list of record sets for a particular hosted zone.
            /// </summary>
            /// <param name="hostedZoneId">The ID of the hosted zone whos record sets you want to list</param>
            /// <param name="settings">The <see cref="Route53Settings"/> required to connect to Route53.</param>
            public IList<ResourceRecordSet> GetResourceRecordSets(string hostedZoneId, Route53Settings settings)
            {
                if (String.IsNullOrEmpty(hostedZoneId))
                {
                    throw new ArgumentNullException("hostedZoneId");
                }



                ListResourceRecordSetsRequest request = new ListResourceRecordSetsRequest(hostedZoneId);

                AmazonRoute53Client client = this.GetClient(settings);
                ListResourceRecordSetsResponse response = client.ListResourceRecordSets(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    _Log.Verbose("Listing record sets");
                    return response.ResourceRecordSets;
                }
                else
                {
                    _Log.Error("Could not list record sets");
                    return null;
                }
            }
        #endregion
    }
}
