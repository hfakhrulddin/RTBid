using Microsoft.AspNet.SignalR.Client;
using RTBid.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTBid.Watchdog
{
    class Program
    {
        static HubConnection _connection;
        static IHubProxy _auctionHub;

        static Dictionary<int, DateTime> _mostRecentBidFor = new Dictionary<int, DateTime>();

        static void Main(string[] args)
        {
            // Use ConfigurationManager to grab signalr url from App.config
            _connection = new HubConnection(ConfigurationManager.AppSettings["SignalRUrl"]);
            _auctionHub = _connection.CreateHubProxy("AuctionHub");
            _connection.Start();

            Timer t = new Timer(Update, null, 10000, 1000);

            Console.WriteLine("Any key to stop watchdog..");
            Console.ReadLine();
        }

        static void Update(object sender)
        {
            using (var db = new RTBidDataContext())
            {
                Console.WriteLine("Running update for {0} auctions", db.Auctions.Count());
                foreach (var auction in db.Auctions.ToList())
                {
                    //_auctionHub.Invoke("pageStartUpServer", auction.AuctionId, auction.ClosedTime, auction.Bids.Last().CurrentAmount);
                    try { 
                    // check if auction started
                    if (auction.StartTime > DateTime.Now || auction.ActualClosedTime <= DateTime.Now)
                    {
                        continue;
                    }
                    else
                    {
                        if (auction.StartTime <= DateTime.Now && auction.StartedTime == null)
                        {
                                // this is a bottleneck that needs to be made async (DataBase)
                                auction.StartedTime = DateTime.Now;
                                auction.OpenSoon = false;
                                auction.InAction = true;

                                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();

                            Console.WriteLine("\tTelling clients that auction started");
                            _auctionHub.Invoke("tellClientsThatAuctionStarted", auction.AuctionId);

                            continue;
                        }

                        // check if auction ended
                        if (auction.ClosedTime <= DateTime.Now && auction.ActualClosedTime == null) // we also need to store the "actualclosedtime"
                        {
                            auction.ActualClosedTime = DateTime.Now;

                                auction.InAction = false;
                                auction.ItemSold = true;
            
                                db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            Console.WriteLine("\tTelling clients that auction ended");
                            _auctionHub.Invoke("tellClientsThatAuctionEnded", auction.AuctionId);
                        }

                        // check if there any new bids to let clients know about
                        if (_mostRecentBidFor.Keys.Contains(auction.AuctionId))
                        {
                            if (_mostRecentBidFor[auction.AuctionId] != auction.Bids.Last().TimeStamp)
                            {
                                Console.WriteLine("\tTelling clients about a new bid");
                                _mostRecentBidFor[auction.AuctionId] = auction.Bids.Last().TimeStamp;
                                _auctionHub.Invoke("tellClientsAboutNewBid", auction.Bids.Last().TimeStamp, auction.StartBid);
                            }
                        }
                        else
                        {
                            if (auction.Bids.Count > 0)
                            {
                                Console.WriteLine("\tTelling clients about a new bid");
                                _mostRecentBidFor[auction.AuctionId] = auction.Bids.Last().TimeStamp;
                                _auctionHub.Invoke("tellClientsAboutNewBid", auction.Bids.Last().TimeStamp, auction.Bids.Last().CurrentAmount);
                            }
                        }
                    }
                }
                    catch { }
                }
            }
        }
    }
}
