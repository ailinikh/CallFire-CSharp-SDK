﻿using System;
using CallFire_csharp_sdk.API.Rest;
using CallFire_csharp_sdk.Common;
using CallFire_csharp_sdk.Common.DataManagement;
using CallFire_csharp_sdk.Common.Resource.Mappers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Callfire_csharp_sdk.Tests.BroadcastTest.Rest
{
    [TestFixture]
    public class GetBroadcastScheduleRestClientTest : GetBroadcastScheduleClientTest
    {
        internal HttpClient HttpClientMock;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            HttpClientMock = MockRepository.GenerateMock<HttpClient>();
            Client = new RestBroadcastClient(HttpClientMock);

            BroadcastScheduleId = 1;
            BroadcastSchedule = new CfBroadcastSchedule(BroadcastScheduleId, DateTime.Now, DateTime.Now, "timeZone", DateTime.Now, DateTime.Now, "daysOfWeek");
            
            var broadcastSchedule = BroadcastScheduleMapper.ToSoapBroadcastSchedule(BroadcastSchedule);
            HttpClientMock
                .Stub(j => j.Send(Arg<string>.Is.Equal(String.Format("/broadcast/schedule/{0}", BroadcastScheduleId)),
                    Arg<HttpMethod>.Is.Equal(HttpMethod.Get),
                    Arg<object>.Is.Null))
                .Return("");//broadcastSchedule);
        }
    }
}
