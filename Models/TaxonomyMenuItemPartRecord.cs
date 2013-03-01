using Orchard.ContentManagement.Records;

namespace Summit.Core.Models {
    public class TaxonomyMenuItemPartRecord : ContentPartRecord {
        public virtual bool RenderMenuItem { get; set; }
        public virtual string Position { get; set; }
        public virtual string Name { get; set; }
    }
}
