﻿using System;
using System.IO;
using System.Xml.Serialization;
using CallFire_csharp_sdk.API.Rest;
using CallFire_csharp_sdk.API.Soap;
using CallFire_csharp_sdk.Common;
using CallFire_csharp_sdk.Common.DataManagement;
using CallFire_csharp_sdk.Common.Resource;
using CallFire_csharp_sdk.Common.Resource.Mappers;
using CallFire_csharp_sdk.Common.Result;
using NUnit.Framework;
using Rhino.Mocks;

namespace Callfire_csharp_sdk.Tests.SubscriptionTest.Rest
{
    [TestFixture]
    public class QuerySubscriptionsRestClientTest : QuerySubscriptionsClientTest
    {
        internal IHttpClient HttpClientMock;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            HttpClientMock = MockRepository.GenerateMock<IHttpClient>();
            Client = new RestSubscriptionClient(HttpClientMock);

            Query = new CfQuery(100, 0);

            SubscriptionFilter = new CfSubscriptionSubscriptionFilter(1, 5, "fromNumber", "toNumber", true);
            Subscription = new CfSubscription(1, true, "endPoint", CfNotificationFormat.Soap,
                CfSubscriptionTriggerEvent.CampaignStarted, SubscriptionFilter);

            var subscriptions = new CfSubscription[1];
            subscriptions[0] = Subscription;
            SubscriptionQueryResult = new CfSubscriptionQueryResult(1, subscriptions);

            GenerateMock(SubscriptionQueryResult, Query);

            subscriptions[0] = null;
            var subscriptionQueryResult = new CfSubscriptionQueryResult(1, subscriptions);
            var query = new CfQuery(100, 1);
            GenerateMock(subscriptionQueryResult, query);
        }

        private void GenerateMock(CfSubscriptionQueryResult subscriptionQueryResult, CfQuery query)
        {
            var resource = new ResourceList();
            var array = new Subscription[1];
            array[0] = SubscriptionMapper.ToSoapSubscription(subscriptionQueryResult.Subscription[0]);
            resource.Resource = array;
            resource.TotalResults = 1;

            var serializer = new XmlSerializer(typeof(ResourceList));
            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, resource);

            HttpClientMock.Stub(j => j.Send(
                            Arg<string>.Is.Equal(String.Format("/subscription?MaxResults={0}&FirstResult={1}",
                                query.MaxResults, query.FirstResult)),
                            Arg<HttpMethod>.Is.Equal(HttpMethod.Get),
                            Arg<object>.Is.Null))
                .Return(writer.ToString());
        }
    }
}
