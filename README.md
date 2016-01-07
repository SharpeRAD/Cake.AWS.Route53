# Cake.AWS.Route53
Cake Build addon for managing Amazon Route53 DNS records

[![Build status](https://ci.appveyor.com/api/projects/status/ds56nw3ffa7t5bfp?svg=true)](https://ci.appveyor.com/project/PhillipSharpe/cake-aws-route53)

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

[![Join the chat at https://gitter.im/cake-build/cake](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/cake-build/cake?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)



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

A complete Cake example can be found [here](https://github.com/SharpeRAD/Cake.AWS.Route53/blob/master/test/build.cake)
