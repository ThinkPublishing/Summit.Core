// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationPartHandler.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   handler for destination part
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Handlers
{
    using System.Web.Routing;

    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;
    using Orchard.ContentManagement.Handlers;

    using Summit.Core.Models;
    using Summit.Core.Routing;

    [UsedImplicitly]
    public class DestinationPartHandler : ContentHandler
    {
        private readonly IDestinationPathConstraint destinationPathConstraint;

        public DestinationPartHandler(IDestinationPathConstraint destinationPathConstraint)
        {
            this.destinationPathConstraint = destinationPathConstraint;

            OnPublished<DestinationPart>(
                (context, destination) => this.destinationPathConstraint.AddPath(destination.As<IAliasAspect>().Path));
            OnUnpublished<DestinationPart>(
                (context, destination) => this.destinationPathConstraint.RemovePath(destination.As<IAliasAspect>().Path));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            var destination = context.ContentItem.As<DestinationPart>();

            if (destination == null) return;

            context.Metadata.DisplayRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "Destination" },
                    { "Action", "Item" },
                    { "destinationId", context.ContentItem.Id }
                };
            context.Metadata.CreateRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" }, 
                    { "Controller", "DestinationAdmin" },
                    { "Action", "Create" }
                };
            context.Metadata.EditorRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "DestinationAdmin" },
                    { "Action", "Edit" },
                    { "destinationId", context.ContentItem.Id }
                };
            context.Metadata.RemoveRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "DestinationAdmin" },
                    { "Action", "Remove" },
                    { "destinationId", context.ContentItem.Id }
                };
            context.Metadata.AdminRouteValues = new RouteValueDictionary
                {
                    { "Area", "Summit.Core" },
                    { "Controller", "DestinationAdmin" },
                    { "Action", "Item" },
                    { "destinationId", context.ContentItem.Id }
                };
        }
    }
}