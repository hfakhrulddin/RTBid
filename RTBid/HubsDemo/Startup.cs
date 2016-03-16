using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System;
using RTBid;

[assembly: OwinStartup(typeof(RTBidManager.Api.Startup))]


namespace RTBidManager.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); //SignalR Added
 
        }
    }
}