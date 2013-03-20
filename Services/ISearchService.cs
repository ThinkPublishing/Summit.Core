// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services
{
    using System.Collections.Generic;

    using Orchard;
    using Orchard.ContentManagement;

    public interface ISearchService : IDependency {
        int SearchCount(string search);
        IEnumerable<IContent> Search(string search, int? skip, int? count);
        IEnumerable<IContent> SearchBySnippet(string snippet);
    }
}