// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyMenuItemPartHandler.cs" company="Zaust">
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
    using Orchard.Environment.Extensions;

    using Summit.Core.Models;

    [OrchardFeature("TaxonomyMenuItem")]
    public class TaxonomyMenuItemPartHandler : ContentHandler
    {
        public TaxonomyMenuItemPartHandler(IRepository<TaxonomyMenuItemPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<TaxonomyMenuItemPart>("Taxonomy"));
        }
    }
}