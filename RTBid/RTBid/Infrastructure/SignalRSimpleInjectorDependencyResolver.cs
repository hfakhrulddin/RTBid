//using Microsoft.AspNet.SignalR;
//using SimpleInjector;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace RTBid.Infrastructure
//{
//    public class SignalRSimpleInjectorDependencyResolver : DefaultDependencyResolver
//    {
//        private readonly Container _container;
//        public SignalRSimpleInjectorDependencyResolver(Container container)
//        {
//            _container = container;
//        }
//        public override object GetService(Type serviceType)
//        {
//            return ((IServiceProvider)_container).GetService(serviceType)
//                   ?? base.GetService(serviceType);
//        }

//        public override IEnumerable<object> GetServices(Type serviceType)
//        {
//            return _container.GetAllInstances(serviceType)
//                .Concat(base.GetServices(serviceType));
//        }
//    }
//}