// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyPartRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.ContentManagement.Records;

    public class TaxonomyPartRecord : ContentPartRecord 
    {
        public virtual string TermTypeName { get; set; }
    }
}
