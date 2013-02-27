// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Routes.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   routes
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Orchard.Mvc.Routes;

    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in this.GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
                {
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Search",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "Search" },
                                        { "action", "Index" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "SearchBySnippet/{snippet}",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "Search" },
                                        { "action", "Find" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/Create",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "DestinationAdmin" },
                                        { "action", "Create" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Edit",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "DestinationAdmin" }, 
                                        { "action", "Edit" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Remove",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "DestinationAdmin" },
                                        { "action", "Remove" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" }, 
                                        { "controller", "DestinationAdmin" }, 
                                        { "action", "Item" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Hotels/Create",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "HotelAdmin" },
                                        { "action", "Create" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Hotels/{hotelId}/Edit",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "HotelAdmin" },
                                        { "action", "Edit" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Hotels/{hotelId}/Delete",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "HotelAdmin" },
                                        { "action", "Delete" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Hotels/{hotelId}/Publish",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "HotelAdmin" },
                                        { "action", "Publish" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations/{destinationId}/Hotels/{hotelId}/Unpublish",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "HotelAdmin" },
                                        { "action", "Unpublish" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Concierges/Create",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "ConciergeAdmin" },
                                        { "action", "Create" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Concierges/Create/{destinationId}",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" },
                                        { "controller", "ConciergeAdmin" },
                                        { "action", "Create" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        },
                    new RouteDescriptor
                        {
                            Route =
                                new Route(
                                "Admin/Destinations",
                                new RouteValueDictionary
                                    {
                                        { "area", "Summit.Core" }, 
                                        { "controller", "DestinationAdmin" }, 
                                        { "action", "List" }
                                    },
                                new RouteValueDictionary(),
                                new RouteValueDictionary { { "area", "Summit.Core" } },
                                new MvcRouteHandler())
                        }
                };
        }
    }
}