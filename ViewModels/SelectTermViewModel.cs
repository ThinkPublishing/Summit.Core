// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTermViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels {
    using System.Collections.Generic;

    using Summit.Core.Models;

    public class SelectTermViewModel {
        public IEnumerable<TermPart> Terms { get; set; }
        public int SelectedTermId { get; set; }
    }
}
