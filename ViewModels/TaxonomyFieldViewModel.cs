using System.Collections.Generic;
using Summit.Core.Settings;

namespace Summit.Core.ViewModels {
    public class TaxonomyFieldViewModel {
        public string Name { get; set; }
        public TaxonomyFieldSettings Settings { get; set; }
        public IList<TermEntry> Terms { get; set; }
        public int SingleTermId { get; set; }
    }
}