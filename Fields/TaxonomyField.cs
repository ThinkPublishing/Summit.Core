// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyField.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Fields
{
    using System.Collections.Generic;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Utilities;

    using Summit.Core.Models;

    /// <summary>
    /// This field has not state, as all terms are saved using <see cref="TermContentItem"/>
    /// </summary>
    public class TaxonomyField : ContentField
    {

        public TaxonomyField()
        {
            Terms = new LazyField<IEnumerable<TermPart>>();
        }

        /// <summary>
        /// Gets the Terms associated with this field
        /// </summary>
        public LazyField<IEnumerable<TermPart>> Terms { get; set; }
    }
}