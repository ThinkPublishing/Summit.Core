using Summit.Core.Models;
using System.Collections.Generic;

namespace Summit.Core.ViewModels {
    public class SelectTermViewModel {
        public IEnumerable<TermPart> Terms { get; set; }
        public int SelectedTermId { get; set; }
    }
}
