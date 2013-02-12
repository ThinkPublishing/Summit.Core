// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationPartDriver.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   driver for destination part
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers
{
    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;

    using Summit.Core.Models;

    [UsedImplicitly]
    public class DestinationPartDriver : ContentPartDriver<DestinationPart>
    {
        protected override string Prefix
        {
            get
            {
                return "DestinationPart";
            }
        }

        protected override DriverResult Display(DestinationPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Destinations_Destination_Manage", () => shapeHelper.Parts_Destinations_Destination_Manage()),
                ContentShape("Parts_Destinations_Destination_SummaryAdmin", () => shapeHelper.Parts_Destinations_Destination_SummaryAdmin()));
        }

        protected override DriverResult Editor(DestinationPart blogPart, dynamic shapeHelper)
        {
            return blogPart.Id > 0 ? this.ContentShape("Destination_DeleteButton", deleteButton => deleteButton) : null;
        }

        protected override DriverResult Editor(DestinationPart blogPart, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(blogPart, Prefix, null, null);
            return Editor(blogPart, shapeHelper);
        }
    }
}