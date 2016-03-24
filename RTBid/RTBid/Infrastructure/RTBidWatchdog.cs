using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RTBid.Hubs;
using System;
using System.Threading;
using System.Web.Hosting;

namespace RTBid.Infrastructure
{
    public class RTBidWatchdog : IRegisteredObject
    {
        private Timer _timer;
        private IHubContext _hub;

        public RTBidWatchdog()
        {
            _hub = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();

            _timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));
        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();
            HostingEnvironment.UnregisterObject(this);
        }

        private void OnTimerElapsed(object sender)
        {
            _hub.Clients.All.heartbeat();
        }
    }
}


/////
//public class BackgroundUptimeServerTimer : IRegisteredObject
//{
//    private readonly DateTime _internetBirthDate = On.October.The29th.In(1969);
//    private readonly IHubContext _uptimeHub;
//    private Timer _timer;


//    public BackgroundUptimeServerTimer()
//    {
//        _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<UptimeHub>();

//        StartTimer();
//    }


//    private void StartTimer()
//    {
//        var delayStartby = 2.Seconds();
//        var repeatEvery = 1.Seconds();

//        _timer = new Timer(BroadcastUptimeToClients, null, delayStartby, repeatEvery);
//    }



//    private void BroadcastUptimeToClients(object state)
//    {
//        TimeSpan uptime = DateTime.Now - _internetBirthDate;

//        _uptimeHub.Clients.All.internetUpTime(uptime.Humanize(5));
//    }


//}