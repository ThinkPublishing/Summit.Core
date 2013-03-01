using Summit.Core.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Summit.Core.Handlers {
    public class TermWidgetPartHandler : ContentHandler {
        public TermWidgetPartHandler(IRepository<TermWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}