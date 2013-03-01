using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Summit.Core.Models;
using Orchard.Environment.Extensions;

namespace Summit.Core.Handlers {
    [OrchardFeature("TaxonomyMenuItem")]
    public class TaxonomyMenuItemPartHandler : ContentHandler {
        public TaxonomyMenuItemPartHandler(IRepository<TaxonomyMenuItemPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<TaxonomyMenuItemPart>("Taxonomy"));
        }
    }
}