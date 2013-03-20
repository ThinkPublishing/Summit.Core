// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermWidgetViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Summit.Core.Models;

    public class TermWidgetViewModel {
        public SelectList AvailableTaxonomies { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "You must select a taxonomy")]
        public int SelectedTaxonomyId { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "You must select a term")]
        public int SelectedTermId { get; set; }

        public bool Ascending { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Count { get; set; }
        public string FieldName { get; set; }
        public string OrderBy { get; set; }

        public TermWidgetPart Part { get; set; }
        public IEnumerable<string> ContentTypeNames { get; set; }
    }
}
