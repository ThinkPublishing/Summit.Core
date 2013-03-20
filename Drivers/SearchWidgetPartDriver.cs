// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchWidgetPartDriver.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   search widget part driver
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers 
{
    using JetBrains.Annotations;

    using Orchard.ContentManagement.Drivers;

    using Summit.Core.Models;

    [UsedImplicitly]
    public class SearchWidgetPartDriver : ContentPartDriver<SearchWidgetPart> 
    {
        protected override string Prefix
        {
            get
            {
                return "SearchWidgetPart";
            }
        }

        protected override DriverResult Display(SearchWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return this.ContentShape("Parts_SearchWidget", () => shapeHelper.Parts_SearchWidget());
        }
    }
}