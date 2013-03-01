using Summit.Core.Routing;
using Summit.Core.Services;
using JetBrains.Annotations;
using Summit.Core.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Summit.Core.Handlers {
    [UsedImplicitly]
    public class TaxonomyPartHandler : ContentHandler {

        public TaxonomyPartHandler(
            IRepository<TaxonomyPartRecord> repository,
            ITaxonomySlugConstraint taxonomySlugConstraint,
            ITaxonomyService taxonomyService
            ) {

            Filters.Add(StorageFilter.For(repository));

            OnPublished<TaxonomyPart>(
                (context, part) => {
                    taxonomySlugConstraint.AddSlug(part.Slug);
                    taxonomyService.CreateTermContentType(part);
                });

            OnUnpublishing<TaxonomyPart>(
                (context, part) =>
                    taxonomySlugConstraint.RemoveSlug(part.Slug)
                );
        }
    }
}