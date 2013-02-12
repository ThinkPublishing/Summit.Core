// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotelService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   hotel service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Common.Models;
    using Orchard.Tasks.Scheduling;

    using Summit.Core.Models;

    [UsedImplicitly]
    public class HotelService : IHotelService
    {
        private readonly IContentManager contentManager;

        private readonly IPublishingTaskManager publishingTaskManager;

        public HotelService(
            IContentManager contentManager,
            IPublishingTaskManager publishingTaskManager,
            IContentDefinitionManager contentDefinitionManager)
        {
            this.contentManager = contentManager;
            this.publishingTaskManager = publishingTaskManager;
        }

        public HotelPart Get(int id)
        {
            return Get(id, VersionOptions.Published);
        }

        public HotelPart Get(int id, VersionOptions versionOptions)
        {
            return contentManager.Get<HotelPart>(id, versionOptions);
        }

        public IEnumerable<HotelPart> Get(DestinationPart destinationPart)
        {
            return Get(destinationPart, VersionOptions.Published);
        }

        public IEnumerable<HotelPart> Get(DestinationPart destinationPart, VersionOptions versionOptions)
        {
            return GetHotelQuery(destinationPart, versionOptions).List().Select(ci => ci.As<HotelPart>());
        }

        public IEnumerable<HotelPart> Get(DestinationPart destinationPart, int skip, int count)
        {
            return Get(destinationPart, skip, count, VersionOptions.Published);
        }

        public IEnumerable<HotelPart> Get(
            DestinationPart destinationPart, int skip, int count, VersionOptions versionOptions)
        {
            return
                GetHotelQuery(destinationPart, versionOptions).Slice(skip, count).ToList().Select(
                    ci => ci.As<HotelPart>());
        }

        public int HotelCount(DestinationPart destinationPart)
        {
            return HotelCount(destinationPart, VersionOptions.Published);
        }

        public int HotelCount(DestinationPart destinationPart, VersionOptions versionOptions)
        {
            return GetHotelQuery(destinationPart, versionOptions).Count();
        }

        public void Delete(HotelPart hotelPart)
        {
            publishingTaskManager.DeleteTasks(hotelPart.ContentItem);
            contentManager.Remove(hotelPart.ContentItem);
        }

        public void Publish(HotelPart hotelPart)
        {
            publishingTaskManager.DeleteTasks(hotelPart.ContentItem);
            contentManager.Publish(hotelPart.ContentItem);
        }

        public void Publish(HotelPart hotelPart, DateTime scheduledPublishUtc)
        {
            publishingTaskManager.Publish(hotelPart.ContentItem, scheduledPublishUtc);
        }

        public void Unpublish(HotelPart hotelPart)
        {
            contentManager.Unpublish(hotelPart.ContentItem);
        }

        public DateTime? GetScheduledPublishUtc(HotelPart hotelPart)
        {
            var task = publishingTaskManager.GetPublishTask(hotelPart.ContentItem);
            return (task == null ? null : task.ScheduledUtc);
        }

        private IContentQuery<ContentItem, CommonPartRecord> GetHotelQuery(
            DestinationPart destinationPart, VersionOptions versionOptions)
        {
            return
                contentManager.Query(versionOptions, "Hotel").Join<CommonPartRecord>().Where(
                    cr => cr.Container == destinationPart.ContentItem.Record).OrderByDescending(cr => cr.CreatedUtc).
                    WithQueryHintsFor("Hotel");
        }
    }
}