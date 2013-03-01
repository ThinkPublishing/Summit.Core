using Summit.Core.Services;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.Drivers;
using Summit.Core.Models;
using Orchard.Localization;

namespace Summit.Core.Drivers {
    public class TaxonomyPartDriver : ContentPartDriver<TaxonomyPart> {
        private readonly ITaxonomyService _taxonomyService;

        public TaxonomyPartDriver(ITaxonomyService taxonomyService) {
            _taxonomyService = taxonomyService;
        }

        protected override string Prefix { get { return "Taxonomy"; } }
        public Localizer T { get; set; }

        protected override DriverResult Editor(TaxonomyPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper) {
            TaxonomyPart existing = _taxonomyService.GetTaxonomyByName(part.Name);

            if (existing != null && existing.Record != part.Record) {
                updater.AddModelError("Title", T("A taxonomy with the same name already exists"));
            }
            
            // nothing to display for this part
            return null;
        }

        protected override void Exporting(TaxonomyPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("TermTypeName", part.TermTypeName);
        }

        protected override void Importing(TaxonomyPart part, ImportContentContext context) {
            part.TermTypeName = context.Attribute(part.PartDefinition.Name, "TermTypeName");
        }
    }
}