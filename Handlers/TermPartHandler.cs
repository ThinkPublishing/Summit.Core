// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermPartHandler.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Handlers
{
    using Summit.Core.Routing;
    using Summit.Core.Services;
    using Summit.Core.Models;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    public class TermPartHandler : ContentHandler
    {
        public TermPartHandler(
            IRepository<TermPartRecord> repository,
            ITaxonomyService taxonomyService,
            ITermPathConstraint termPathConstraint)
        {
            Filters.Add(StorageFilter.For(repository));

            OnRemoved<IContent>((context, tags) => taxonomyService.DeleteAssociatedTerms(context.ContentItem));

            OnInitializing<TermPart>((context, part) => part.Selectable = true);

            OnPublished<TermPart>(
                (context, part) =>
                    {
                        termPathConstraint.AddPath(part.Slug);
                        foreach (var child in taxonomyService.GetChildren(part))
                        {
                            termPathConstraint.AddPath(child.Slug);
                        }
                    });

            OnUnpublishing<TermPart>((context, part) => termPathConstraint.RemovePath(part.Slug));
        }
    }
}