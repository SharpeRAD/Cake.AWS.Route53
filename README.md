# Cake.AWS.Route53
Cake Build addin for managing Amazon Route53 DNS records

[![Build status](https://ci.appveyor.com/api/projects/status/ds56nw3ffa7t5bfp?svg=true)](https://ci.appveyor.com/project/SharpeRAD/cake-aws-route53)

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

[![Join the chat at https://gitter.im/cake-build/cake](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/cake-build/cake)



## Table of contents

1. [Implemented functionality](https://github.com/SharpeRAD/Cake.AWS.Route53#implemented-functionality)
2. [Referencing](https://github.com/SharpeRAD/Cake.AWS.Route53#referencing)
3. [Usage](https://github.com/SharpeRAD/Cake.AWS.Route53#usage)
4. [Example](https://github.com/SharpeRAD/Cake.AWS.Route53#example)
5. [Plays well with](https://github.com/SharpeRAD/Cake.AWS.Route53#plays-well-with)
6. [License](https://github.com/SharpeRAD/Cake.AWS.Route53#license)
7. [Share the love](https://github.com/SharpeRAD/Cake.AWS.Route53#share-the-love)



## Implemented functionality

* Create HostedZone
* Delete HostedZone
* Get HostedZone
* Get HostedZones
* Create Resource RecordSet
* Delete Resource RecordSet
* Get Resource RecordSets



## Referencing

[![NuGet Version](http://img.shields.io/nuget/v/Cake.AWS.Route53.svg?style=flat)](https://www.nuget.org/packages/Cake.AWS.Route53/) [![NuGet Downloads](http://img.shields.io/nuget/dt/Cake.AWS.Route53.svg?style=flat)](https://www.nuget.org/packages/Cake.AWS.Route53/)

Cake.AWS.Route53 is available as a nuget package from the package manager console:

```csharp
Install-Package Cake.AWS.Route53
```

or directly in your build script via a cake addin:

```csharp
#addin "Cake.AWS.Route53"
```



## Usage

```csharp
#addin "Cake.AWS.Route53"

Task("Create-Hosted-Zone")
    .Description("Creates a hosted zone for a particular domain")
    .Does(() =>
{
    CreateHostedZone("test.com", new Route53Settings()
    {
        AccessKey = "blah",
        SecretKey = "blah",
        Region = RegionEndpoint.EUWest1
    });
});

Task("Create-Resource-RecordSet")
    .Description("Create or change a DNS record for a hosted zone")
    .Does(() =>
{
    CreateResourceRecordSet("hostedZoneId", "wwww", RRType.A, "192.168.42.123", 3600, new Route53Settings()
    {
        AccessKey = "blah",
        SecretKey = "blah",
        Region = RegionEndpoint.EUWest1
    });
});

RunTarget("Create-Resource-RecordSet");
```



## Example

A complete Cake example can be found [here](https://github.com/SharpeRAD/Cake.AWS.Route53/blob/master/test/build.cake).



## Plays well with

If your routing traffic to EC2 instances its worth checking out [Cake.AWS.EC2](https://github.com/SharpeRAD/Cake.AWS.EC2) or if your using ELB load balancers check out [Cake.AWS.ElasticLoadBalancing](https://github.com/SharpeRAD/Cake.AWS.ElasticLoadBalancing).

If your looking for a way to trigger cake tasks based on windows events or at scheduled intervals then check out [CakeBoss](https://github.com/SharpeRAD/CakeBoss).



## License

Copyright (c) 2015 - 2016 Phillip Sharpe

Cake.AWS.Route53 is provided as-is under the MIT license. For more information see [LICENSE](https://github.com/SharpeRAD/Cake.AWS.Route53/blob/master/LICENSE).



## Share the love

If this project helps you in anyway then please :star: the repository.
