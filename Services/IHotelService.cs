// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHotelService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   hotel service interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services 
{
    using System;
    using System.Collections.Generic;

    using Orchard;
    using Orchard.ContentManagement;

    using Summit.Core.Models;

    public interface IHotelService : IDependency {
        HotelPart Get(int id);
        HotelPart Get(int id, VersionOptions versionOptions);
        IEnumerable<HotelPart> Get(DestinationPart destinationPart);
        IEnumerable<HotelPart> Get(DestinationPart destinationPart, VersionOptions versionOptions);
        IEnumerable<HotelPart> Get(DestinationPart destinationPart, int skip, int count);
        IEnumerable<HotelPart> Get(DestinationPart destinationPart, int skip, int count, VersionOptions versionOptions);
        int HotelCount(DestinationPart destinationPart);
        int HotelCount(DestinationPart destinationPart, VersionOptions versionOptions);
        void Delete(HotelPart blogPostPart);
        void Publish(HotelPart blogPostPart);
        void Publish(HotelPart blogPostPart, DateTime scheduledPublishUtc);
        void Unpublish(HotelPart blogPostPart);
        DateTime? GetScheduledPublishUtc(HotelPart blogPostPart);

    }
}