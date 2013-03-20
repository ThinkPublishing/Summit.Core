// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermAdminIndexViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.Collections.Generic;

    using Orchard.ContentManagement;

    using Summit.Core.Models;

    public class TermAdminIndexViewModel {
        public IList<TermEntry> Terms { get; set; }
        public TermsAdminIndexBulkAction BulkAction { get; set; }
        public TaxonomyPart Taxonomy { get; set; }
        public int TaxonomyId { get; set; }
    }

    public class TermEntry {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Selectable { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
        public bool IsChecked { get; set; }
        public ContentItem ContentItem { get; set; }
    }

    public enum TermsAdminIndexBulkAction {
        None,
        Delete,
    }
}
