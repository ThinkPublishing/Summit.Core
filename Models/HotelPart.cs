// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotelPart.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   hotel part definition
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models
{
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;

    public class HotelPart : ContentPart
    {
        public DestinationPart DestinationPart 
        {
            get
            {
                return this.As<ICommonPart>().Container.As<DestinationPart>();
            }
            set
            {
                this.As<ICommonPart>().Container = value;
            }
        }
    }
}