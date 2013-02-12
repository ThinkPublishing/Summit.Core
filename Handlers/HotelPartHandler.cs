// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotelPartHandler.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   handler for hotelpart
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Handlers
{
    using System.Linq;
    using System.Web.Routing;

    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Handlers;

    using Summit.Core.Models;
    using Summit.Core.Services;

    [UsedImplicitly]
    public class HotelPartHandler : ContentHandler
    {
        private readonly IDestinationService destinationService;

        private readonly IHotelService hotelService;

        public HotelPartHandler(
            IDestinationService destinationService, IHotelService hotelService, RequestContext requestContext)
        {
            this.destinationService = destinationService;
            this.hotelService = hotelService;

            OnGetDisplayShape<HotelPart>(SetModelProperties);
            OnGetEditorShape<HotelPart>(SetModelProperties);
            OnUpdateEditorShape<HotelPart>(SetModelProperties);

            //OnCreated<HotelPart>((context, part) => UpdateBlogPostCount(part));
            //OnPublished<HotelPart>((context, part) => UpdateBlogPostCount(part));
            //OnUnpublished<HotelPart>((context, part) => UpdateBlogPostCount(part));
            //OnVersioned<HotelPart>((context, part, newVersionPart) => UpdateBlogPostCount(newVersionPart));
            //OnRemoved<HotelPart>((context, part) => UpdateBlogPostCount(part));

            OnRemoved<DestinationPart>(
                (context, b) =>
                hotelService.Get(context.ContentItem.As<DestinationPart>()).ToList().ForEach(
                    blogPost => context.ContentManager.Remove(blogPost.ContentItem)));
        }

        //private void UpdateHotelCount(HotelPart hotelPart)
        //{
        //    var commonPart = hotelPart.As<CommonPart>();
        //    if (commonPart != null && commonPart.Record.Container != null)
        //    {

        //        var destinationPart = hotelPart.DestinationPart
        //                              ??
        //                              this.destinationService.Get(commonPart.Record.Container.Id, VersionOptions.Published).
        //                                  As<DestinationPart>();

        //        // Ensure the "right" set of published posts for the blog is obtained
        //        destinationPart.ContentItem.ContentManager.Flush();
        //        destinationPart.PostCount = hotelService.PostCount(destinationPart);
        //    }
        //}

        private static void SetModelProperties(BuildShapeContext context, HotelPart hotelPart)
        {
            context.Shape.Blog = hotelPart.DestinationPart;
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            var hotel = context.ContentItem.As<HotelPart>();

            if (hotel == null) return;

            context.Metadata.CreateRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "HotelAdmin" },
                    { "Action", "Create" },
                    { "destinationId", hotel.DestinationPart.Id }
                };
            context.Metadata.EditorRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "HotelAdmin" },
                    { "Action", "Edit" },
                    { "hotelId", context.ContentItem.Id },
                    { "destinationId", hotel.DestinationPart.Id }
                };
            context.Metadata.RemoveRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "HotelAdmin" },
                    { "Action", "Delete" },
                    { "hotelId", context.ContentItem.Id },
                    { "destinationId", hotel.DestinationPart.Id }
                };
        }
    }
}