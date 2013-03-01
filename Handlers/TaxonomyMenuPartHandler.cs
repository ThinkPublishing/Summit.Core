using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Summit.Core.Models;

namespace Summit.Core.Handlers {
    public class TaxonomyMenuPartHandler : ContentHandler {
        public TaxonomyMenuPartHandler(IRepository<TaxonomyMenuPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}