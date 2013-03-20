// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyFieldViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.Collections.Generic;

    using Summit.Core.Settings;

    public class TaxonomyFieldViewModel {
        public string Name { get; set; }
        public TaxonomyFieldSettings Settings { get; set; }
        public IList<TermEntry> Terms { get; set; }
        public int SingleTermId { get; set; }
    }
}