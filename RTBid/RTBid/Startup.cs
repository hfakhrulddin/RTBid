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

/////using Microsoft.Owin;
[assembly: OwinStartup(typeof(RTBidManager.Api.Startup))]

namespace RTBidManager.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            var container = ConfigureSimpleInjector(app);
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            app.MapSignalR(); //SignalR Added



            LogManager.GetLogger("").Info("SignalRChat Initializing.");
            // Branch the pipeline here for requests that start with "/signalr"
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJSONP = true,
                    EnableJavaScriptProxies = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
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
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            /////Database Mapping
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();

            //// The Project Domin Repository Mapping
            container.Register<IAuctionRepository, AuctionRepository>();
            container.Register<IBidRepository, BidRepository>();
            container.Register<ICategoryRepository, CategoryRepository>();
            container.Register<ICommentRepository, CommentRepository>();
            container.Register<IProductRepository, ProductRepository>();
            container.Register<IPurchaseRepository, PurchaseRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<IUserRoleRepository, UserRoleRepository>();
            container.Register<IRTBidUserRepository, RTBidUserRepository>();
            container.Register<IUserAuctionRepository, UserAuctionRepository>();

            //// Authorization Method Mapping 
            container.Register<IUserStore<RTBidUser, int>, UserStore>(Lifestyle.Scoped);
            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);

            /////The Payment Method Mapping
            container.Register<IPaymentService, StripePaymentService>();

            // more code to facilitate a scoped lifestyle
            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}
