using AutoMapper;
using RTBid.Core.Domain;
using RTBid.Core.Models;
using RTBid.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;

namespace RTBid
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute
                (
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            // Call the Method below
            CreateMaps(); 
            //Heart Beat for Hubs CR!!!
        }

        public static void CreateMaps()
        {
            Mapper.CreateMap<Auction, AuctionModel>();
            Mapper.CreateMap<Bid, BidModel>();
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<Comment, CommentModel>();
            Mapper.CreateMap<Product, ProductModel>();
            Mapper.CreateMap<Purchase, PurchaseModel>();
            Mapper.CreateMap<RTBidUser, RTBidUserModel>();
            Mapper.CreateMap<RTBidUser, RTBidUserModel.Profile>();
        }
    }
}