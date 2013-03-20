// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyMenuItemPartRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.ContentManagement.Records;

    public class TaxonomyMenuItemPartRecord : ContentPartRecord 
    {
        public virtual bool RenderMenuItem { get; set; }
        public virtual string Position { get; set; }
        public virtual string Name { get; set; }
    }
}
