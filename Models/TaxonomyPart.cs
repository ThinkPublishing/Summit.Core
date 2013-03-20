// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyPart.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models 
{
    using Orchard.Autoroute.Models;
    using Orchard.ContentManagement;
    using Orchard.Core.Title.Models;

    public class TaxonomyPart : ContentPart<TaxonomyPartRecord> 
    {
        public string Name 
        {
            get { return this.As<TitlePart>().Title; }
            set { this.As<TitlePart>().Title = value; }
        }

        public string Slug 
        {
            get { return this.As<AutoroutePart>().DisplayAlias; }
            set { this.As<AutoroutePart>().DisplayAlias = value; }
        }

        public string TermTypeName 
        {
            get { return Record.TermTypeName; }
            set { Record.TermTypeName = value; }
        }
    }
}
