#region Using Statements
using System.IO;

using Cake.Core;

using NSubstitute;
#endregion



namespace Cake.AWS.Route53.Tests
{
    internal static class CakeHelper
    {
        #region Methods
        public static ICakeEnvironment CreateEnvironment()
        {
            var environment = Substitute.For<ICakeEnvironment>();
            environment.WorkingDirectory = Directory.GetCurrentDirectory();

            return environment;
        }



        public static IRoute53Manager CreateTransferManager()
        {
            return new Route53Manager(CakeHelper.CreateEnvironment(), new DebugLog());
        }
        #endregion
    }
}
