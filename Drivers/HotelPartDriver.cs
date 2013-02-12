// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotelPartDriver.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   driver for hotel part
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers 
{
    using JetBrains.Annotations;

    using Orchard.ContentManagement.Drivers;

    using Summit.Core.Models;

    [UsedImplicitly]
    public class HotelPartDriver : ContentPartDriver<HotelPart> {

        protected override DriverResult Display(HotelPart part, string displayType, dynamic shapeHelper) {
            return null;
        }
    }
}