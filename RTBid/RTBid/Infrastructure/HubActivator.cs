//using Microsoft.AspNet.SignalR.Hubs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace RTBid.Infrastructure
//{
//    public class HubActivator : IHubActivator
//    {
//        readonly Dictionary<Type, Func<IHub>> m_resolvers = new Dictionary<Type, Func<IHub>>();

//        public IHub Create(HubDescriptor descriptor)
//        {
//            if (descriptor == null)
//            {
//                throw new ArgumentNullException("descriptor");
//            }

//            if (descriptor.HubType == null)
//            {
//                return null;
//            }

//            //Func<IHub> resolver = m_resolvers.GetValueOrDefault(descriptor.HubType);

//            return (IHub)Activator.CreateInstance(descriptor.HubType);
//        }

//        public virtual void Register(Type hubType, Func<IHub> activator)
//        {
//            m_resolvers[hubType] = activator;
//        }
//    }
//}