using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using RTBid;
using RTBid.Core.Domain;
using RTBid.Core.Infrastructure;
using RTBid.Core.Repository;
using RTBid.Core.Services.Finance;
using RTBid.Data.Infrastructure;
using RTBid.Data.Repository;
using RTBid.Infrastructure;
using Microsoft.AspNet.SignalR;
using log4net;
using Microsoft.AspNet.SignalR.Hubs;
using RTBid.Hubs;
using SimpleInjector.Diagnostics;


/////using Microsoft.Owin;
[assembly: OwinStartup(typeof(RTBidManager.Api.Startup))]

namespace RTBidManager.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var Container = ConfigureSimpleInjector(app);
            ConfigureOAuth(app, Container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container)
            };


        WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            LogManager.GetLogger("").Info("SignalRChat Initializing.");

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
           
                var hubConfiguration = new HubConfiguration
                {
                    //Resolver = new SignalRSimpleInjectorDependencyResolver(Container),
                    EnableDetailedErrors = false,
                    EnableJSONP = true,
                    EnableJavaScriptProxies = true,
                    //Resolver = resolver
                };

                map.RunSignalR(hubConfiguration);

            });
                app.MapSignalR();
        
          }
          
 
        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new RTBidAuthorizationServerProvider(authRepositoryFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var Container = new Container();

            Container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            /////Database Mapping
            Container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            Container.Register<IUnitOfWork, UnitOfWork>();///???????

            //// The Project Domin Repository Mapping
            Container.Register<IAuctionRepository, AuctionRepository>();/////CR
            Container.Register<IBidRepository, BidRepository>();
            Container.Register<ICategoryRepository, CategoryRepository>();
            Container.Register<ICommentRepository, CommentRepository>();
            Container.Register<IProductRepository, ProductRepository>();
            Container.Register<IPurchaseRepository, PurchaseRepository>();
            Container.Register<IRoleRepository, RoleRepository>();
            Container.Register<IUserRoleRepository, UserRoleRepository>();
            Container.Register<IRTBidUserRepository, RTBidUserRepository>();
            Container.Register<IUserAuctionRepository, UserAuctionRepository>();

            //// Authorization Method Mapping 
            Container.Register<IUserStore<RTBidUser, int>, UserStore>(Lifestyle.Scoped);
            Container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);

            /////The Payment Method Mapping
            Container.Register<IPaymentService, StripePaymentService>();

            // Register signalr hubs with SimpleInjector
            //Container.Register<AuctionHub>();
            //SimpleInjectorHubActivator activator = new SimpleInjectorHubActivator(Container);
            //GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);

            // more code to facilitate a scoped lifestyle
            app.Use(async (context, next) =>
            {
                using (Container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            Container.Verify();


            return Container;
        }
    }
}
