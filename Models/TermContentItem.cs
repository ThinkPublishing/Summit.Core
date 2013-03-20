// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermContentItem.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.Data.Conventions;

    /// <summary>
    /// Represents a relationship between a Term and a Content Item
    /// </summary>
    public class TermContentItem 
    {
        public virtual int Id { get; set; }
        public virtual string Field { get; set; }
        
        public virtual TermPartRecord TermRecord { get; set; }

        [CascadeAllDeleteOrphan]
        public virtual TermsPartRecord TermsPartRecord { get; set; }
    }
}