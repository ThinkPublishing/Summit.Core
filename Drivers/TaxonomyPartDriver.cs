// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyPartDriver.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers
{
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Localization;

    using Summit.Core.Models;
    using Summit.Core.Services;

    public class TaxonomyPartDriver : ContentPartDriver<TaxonomyPart>
    {
        private readonly ITaxonomyService _taxonomyService;

        public TaxonomyPartDriver(ITaxonomyService taxonomyService)
        {
            _taxonomyService = taxonomyService;
        }

        protected override string Prefix
        {
            get
            {
                return "Taxonomy";
            }
        }

        public Localizer T { get; set; }

        protected override DriverResult Editor(
            TaxonomyPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            TaxonomyPart existing = _taxonomyService.GetTaxonomyByName(part.Name);

            if (existing != null && existing.Record != part.Record)
            {
                updater.AddModelError("Title", T("A taxonomy with the same name already exists"));
            }

            // nothing to display for this part
            return null;
        }

        protected override void Exporting(TaxonomyPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("TermTypeName", part.TermTypeName);
        }

        protected override void Importing(TaxonomyPart part, ImportContentContext context)
        {
            part.TermTypeName = context.Attribute(part.PartDefinition.Name, "TermTypeName");
        }
    }
}