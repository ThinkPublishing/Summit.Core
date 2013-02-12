// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   destination service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;
    using Orchard.Core.Title.Models;

    using Summit.Core.Models;
    using Summit.Core.Routing;

    [UsedImplicitly]
    public class DestinationService : IDestinationService
    {
        private readonly IContentManager contentManager;

        private readonly IDestinationPathConstraint destinationPathConstraint;

        public DestinationService(IContentManager contentManager, IDestinationPathConstraint destinationPathConstraint)
        {
            this.contentManager = contentManager;
            this.destinationPathConstraint = destinationPathConstraint;
        }

        public DestinationPart Get(string path)
        {
            return
                contentManager.Query<DestinationPart>().List().FirstOrDefault(rr => rr.As<IAliasAspect>().Path == path);
        }

        public ContentItem Get(int id, VersionOptions versionOptions)
        {
            return contentManager.Get(id, versionOptions);
        }

        public IEnumerable<DestinationPart> Get()
        {
            return Get(VersionOptions.Published);
        }

        public IEnumerable<DestinationPart> Get(VersionOptions versionOptions)
        {
            return
                contentManager.Query<DestinationPart>(versionOptions).Join<TitlePartRecord>().OrderBy(br => br.Title).
                    List();
        }

        public void Delete(ContentItem blog)
        {
            contentManager.Remove(blog);
            destinationPathConstraint.RemovePath(blog.As<IAliasAspect>().Path);
        }
    }
}