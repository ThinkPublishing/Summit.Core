// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyAdminIndexViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.Collections.Generic;

    using Summit.Core.Models;

    public class TaxonomyAdminIndexViewModel {
        public IList<TaxonomyEntry> Taxonomies { get; set; }
        public TaxonomiesAdminIndexBulkAction BulkAction { get; set; }
    }

    public class TaxonomyEntry {
        public TaxonomyPart Taxonomy { get; set; }
        public bool IsChecked { get; set; }
    }

    public enum TaxonomiesAdminIndexBulkAction {
        None,
        Delete,
    }
}
