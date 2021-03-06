﻿using System;
using CallFire_csharp_sdk.API;
using CallFire_csharp_sdk.Common.DataManagement;
using CallFire_csharp_sdk.Common.Resource;
using NUnit.Framework;

namespace Callfire_csharp_sdk.Tests.BroadcastTest
{
    [TestFixture]
    public abstract class QueryBroadcastClientTest
    {
        protected IBroadcastClient Client;

        protected CfQueryBroadcasts ExpectedQueryBroadcast;
        
        protected long BroadcastId;
        protected string BroadcastName;
        protected DateTime BroadcastLastModified;

        [Test]
        public void Test_QueryBroadcast()
        {
            CfBroadcastType[] broadcastType = { CfBroadcastType.Ivr };
            var cfQueryBroadcasts = new CfQueryBroadcasts(ExpectedQueryBroadcast.MaxResults, ExpectedQueryBroadcast.FirstResult, broadcastType, true, ExpectedQueryBroadcast.LabelName);
            
            var cfBroadcastQueryResult = Client.QueryBroadcasts(cfQueryBroadcasts);
            Assert.IsNotNull(cfBroadcastQueryResult);
        }

        [Test]
        public void Test_QueryBroadcast_properties()
        {
            CfBroadcastType[] broadcastType = { CfBroadcastType.Ivr };
            var cfQueryBroadcasts = new CfQueryBroadcasts(ExpectedQueryBroadcast.MaxResults, ExpectedQueryBroadcast.FirstResult, broadcastType, true, ExpectedQueryBroadcast.LabelName);

            var cfBroadcastQueryResult = Client.QueryBroadcasts(cfQueryBroadcasts);
            Assert.IsNotNull(cfBroadcastQueryResult);

            var broadcast = cfBroadcastQueryResult.Broadcast[0];
            Assert.IsNotNull(broadcast);
            Assert.AreEqual(BroadcastName, broadcast.Name);
            Assert.IsNull(broadcast.Item);
        }
    }
}
