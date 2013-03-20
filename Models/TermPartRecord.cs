// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermPartRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.ContentManagement.Records;

    /// <summary>
    /// Represents a Term of a Taxonomy
    /// </summary>
    public class TermPartRecord : ContentPartRecord 
    {
        public virtual int TaxonomyId { get; set; }

        public virtual string Path { get; set; }
        public virtual int Count { get; set; }
        public virtual bool Selectable { get; set; }
        public virtual int Weight { get; set; }
    }
}
