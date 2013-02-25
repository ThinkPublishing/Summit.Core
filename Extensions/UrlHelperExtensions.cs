// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlHelperExtensions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   url helpers
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Extensions
{
    using System.Web.Mvc;

    using Summit.Core.Models;

    /// <summary>
    /// TODO: (PH:Autoroute) Many of these are or could be redundant (see controllers)
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string Destinations(this UrlHelper urlHelper)
        {
            return urlHelper.Action("List", "Destination", new { area = "Summit.Core" });
        }

        public static string DestinationsForAdmin(this UrlHelper urlHelper)
        {
            return urlHelper.Action("List", "DestinationAdmin", new { area = "Summit.Core" });
        }

        public static string Destination(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action("Item", "Destination", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }

        public static string DestinationForAdmin(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action(
                "Item", "DestinationAdmin", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }

        public static string DestinationCreate(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Create", "DestinationAdmin", new { area = "Summit.Core" });
        }

        public static string DestinationEdit(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action(
                "Edit", "DestinationAdmin", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }

        public static string DestinationRemove(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action(
                "Remove", "DestinationAdmin", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }

        public static string HotelCreate(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action(
                "Create", "HotelAdmin", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }

        public static string HotelCreate(this UrlHelper urlHelper, DestinationPart destinationPart, int? conceirgeId)
        {
            return urlHelper.Action(
                "Create", "HotelAdmin", new { destinationId = destinationPart.Id, conceirgeId = conceirgeId, area = "Summit.Core" });
        }

        public static string HotelCreate(this UrlHelper urlHelper, DestinationPart destinationPart, int? conceirgeId, bool noConceirge)
        {
            return urlHelper.Action(
                "Create", "HotelAdmin", new { destinationId = destinationPart.Id, conceirgeId, noConceirge, area = "Summit.Core" });
        }

        public static string HotelEdit(this UrlHelper urlHelper, HotelPart hotelPart)
        {
            return urlHelper.Action(
                "Edit",
                "HotelAdmin",
                new { destinationId = hotelPart.DestinationPart.Id, hotelId = hotelPart.Id, area = "Summit.Core" });
        }

        public static string HotelDelete(this UrlHelper urlHelper, HotelPart hotelPart)
        {
            return urlHelper.Action(
                "Delete",
                "HotelAdmin",
                new { destinationId = hotelPart.DestinationPart.Id, hotelId = hotelPart.Id, area = "Summit.Core" });
        }

        public static string HotelPublish(this UrlHelper urlHelper, HotelPart hotelPart)
        {
            return urlHelper.Action(
                "Publish",
                "HotelAdmin",
                new { destinationId = hotelPart.DestinationPart.Id, hotelId = hotelPart.Id, area = "Summit.Core" });
        }

        public static string HotelUnpublish(this UrlHelper urlHelper, HotelPart hotelPart)
        {
            return urlHelper.Action(
                "Unpublish",
                "HotelAdmin",
                new { destinationId = hotelPart.DestinationPart.Id, hotelId = hotelPart.Id, area = "Summit.Core" });
        }

        public static string ConceirgeCreate(this UrlHelper urlHelper, DestinationPart destinationPart)
        {
            return urlHelper.Action(
                "Create", "ConceirgeAdmin", new { destinationId = destinationPart.Id, area = "Summit.Core" });
        }
    }
}