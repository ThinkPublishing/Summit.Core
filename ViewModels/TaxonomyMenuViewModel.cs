// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyMenuViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TaxonomyMenuViewModel {
        public SelectList AvailableTaxonomies { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "You must select a taxonomy")]
        public int SelectedTaxonomyId { get; set; }
        public int SelectedTermId { get; set; }

        public bool DisplayTopMenuItem { get; set; }
        [Required, Range(0, 99)]
        public int LevelsToDisplay { get; set; }
        public bool DisplayContentCount { get; set; }
        public bool HideEmptyTerms { get; set; }
    }
}
