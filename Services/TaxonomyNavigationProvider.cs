// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyNavigationProvider.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services {
    using Orchard.ContentManagement;
    using Orchard.Localization;
    using Orchard.UI.Navigation;

    using Summit.Core.Models;

    public class TaxonomyNavigationProvider : INavigationProvider {
        private readonly IContentManager _contentManager;
        private readonly ITaxonomyService _taxonomyService;

        public Localizer T { get; set; }

        public TaxonomyNavigationProvider(IContentManager contentManager, ITaxonomyService taxonomyService) {
            _contentManager = contentManager;
            _taxonomyService = taxonomyService;
        }

        public string MenuName {
            get { return "main"; }
        }

        public void GetNavigation(NavigationBuilder builder) {
            var parts = _contentManager.Query<TaxonomyMenuItemPart, TaxonomyMenuItemPartRecord>()
                .Where(x => x.RenderMenuItem)
                .List();

            foreach(var menuItemPart in parts) {
                var taxonomy = _taxonomyService.GetTaxonomy(menuItemPart.ContentItem.Id);
                foreach(var term in _taxonomyService.GetTerms(menuItemPart.ContentItem.Id)) {
                    builder.Add(new LocalizedString(menuItemPart.Name), menuItemPart.Position,
                                menu => menu.Action("List", "Home", new { area = "Summit.Core", taxonomySlug = taxonomy.Slug})
                                    .Add(T(term.Name),
                                    term.Weight.ToString(),
                                    x => x.Action("Item", "Home", new { area = "Summit.Core", termPath = term.Slug })));
                }
            }
        }
    }
}