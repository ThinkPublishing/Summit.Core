using Orchard.ContentManagement.Records;

namespace Summit.Core.Models {
    public class TaxonomyPartRecord : ContentPartRecord {
        public virtual string TermTypeName { get; set; }
    }
}
