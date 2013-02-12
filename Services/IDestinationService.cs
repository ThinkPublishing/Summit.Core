// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDestinationService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   destination service interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services {
    using System.Collections.Generic;

    using Orchard;
    using Orchard.ContentManagement;

    using Summit.Core.Models;

    public interface IDestinationService : IDependency {
        DestinationPart Get(string path);
        ContentItem Get(int id, VersionOptions versionOptions);
        IEnumerable<DestinationPart> Get();
        IEnumerable<DestinationPart> Get(VersionOptions versionOptions);
        void Delete(ContentItem destination);
    }
}