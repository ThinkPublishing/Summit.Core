// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermsPartRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using System.Collections.Generic;

    using Orchard.ContentManagement.Records;
    using Orchard.Data.Conventions;

    public class TermsPartRecord : ContentPartRecord 
    {
        public TermsPartRecord() {
            Terms = new List<TermContentItem>();
        }

        [CascadeAllDeleteOrphan]
        public virtual IList<TermContentItem> Terms { get; set; }
    }
}