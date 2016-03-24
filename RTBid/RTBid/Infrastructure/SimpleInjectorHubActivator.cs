using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SimpleInjector;



namespace RTBid.Infrastructure
{
    public class SimpleInjectorHubActivator : IHubActivator
    {
        private readonly Container _container;

        public SimpleInjectorHubActivator(
            Container container)
        {
            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (Hub)_container.GetInstance(descriptor.HubType);
        }
    }
}