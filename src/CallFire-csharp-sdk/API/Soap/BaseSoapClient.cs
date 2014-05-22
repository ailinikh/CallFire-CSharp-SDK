﻿using System.ServiceModel;

namespace CallFire_csharp_sdk.API.Soap
{
    internal abstract class BaseSoapClient<T>
        where T: IClient
    {
        private const string SoapEndpointAddress = "https://www.callfire.com/api/1.1/soap12/"; // "http://callfire.com/api/1.1/wsdl/callfire-service-http-soap12.wsdl"; //
        internal readonly IBroadcastServicePortTypeClient BroadcastService;
        internal readonly ISubscriptionServicePortTypeClient SubscriptionService;

        internal BaseSoapClient(string username, string password)
        {
            if (typeof(T) == typeof(IBroadcastClient))
            {
                BroadcastService = CreateBroadcastSoapServiceClient(username, password);
            }
            if (typeof(T) == typeof(ISubscriptionClient))
            {
                SubscriptionService = CreateSubscriptionSoapServiceClient(username, password);
            }
        }

        private static BroadcastServicePortTypeClient CreateBroadcastSoapServiceClient(string username, string password)
        {
            var service = new BroadcastServicePortTypeClient(
                new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential), new EndpointAddress(SoapEndpointAddress))
                {
                    ClientCredentials = { UserName = { UserName = username, Password = password } }
                };
            return service;
        }

        private static SubscriptionServicePortTypeClient CreateSubscriptionSoapServiceClient(string username, string password)
        {
            var service = new SubscriptionServicePortTypeClient(
                new BasicHttpBinding(new BasicHttpSecurityMode()), new EndpointAddress(SoapEndpointAddress))
            {
                ClientCredentials = { UserName = { UserName = username, Password = password } }
            };
            return service;
        }

        internal BaseSoapClient(IBroadcastServicePortTypeClient broadcastService)
        {
            BroadcastService = broadcastService;
        }

        internal BaseSoapClient(ISubscriptionServicePortTypeClient subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }
    }
}
