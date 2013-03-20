// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyMenuPartHandler.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Handlers
{
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    using Summit.Core.Models;

    public class TaxonomyMenuPartHandler : ContentHandler
    {
        public TaxonomyMenuPartHandler(IRepository<TaxonomyMenuPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}