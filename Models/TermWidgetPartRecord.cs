// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermWidgetPartRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.ContentManagement.Records;

    public class TermWidgetPartRecord : ContentPartRecord 
    {
        public TermWidgetPartRecord() {
            Count = 10;
        }

        public virtual TaxonomyPartRecord TaxonomyPartRecord { get; set; }
        public virtual TermPartRecord TermPartRecord { get; set; }
        
        public virtual int Count { get; set; }
        public virtual string OrderBy { get; set; }

        public virtual string FieldName { get; set; }
        public virtual string ContentType { get; set; }
    }
}