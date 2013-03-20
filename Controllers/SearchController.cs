// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   search controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;
    using Orchard.DisplayManagement;
    using Orchard.Localization;
    using Orchard.Mvc;
    using Orchard.Settings;
    using Orchard.Themes;
    using Orchard.UI.Navigation;

    using Summit.Core.Services;

    public class SearchController : Controller
    {
        private readonly ISiteService siteService;

        private readonly ISearchService searchService;

        private readonly IContentManager contentManager;

        public SearchController(
            ISiteService siteService,
            IShapeFactory shapeFactory,
            ISearchService searchService,
            IContentManager contentManager)
        {
            this.siteService = siteService;
            this.searchService = searchService;
            this.contentManager = contentManager;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        [Themed]
        public ActionResult Index(string search, PagerParameters pagerParameters)
        {
            var pager = new Pager(this.siteService.GetSiteSettings(), pagerParameters);

            var searchCount = this.searchService.SearchCount(search);
            var searchResults = this.searchService.Search(search, pager.Page - 1, pager.PageSize);

            var directResult =
                searchResults.FirstOrDefault(x => x.As<ITitleAspect>().Title.ToLower() == search.ToLower());
            if (directResult != null)
            {
                return this.Redirect(directResult.As<IAliasAspect>().Path);
            }

            var searchShapes = searchResults.Select(bp => this.contentManager.BuildDisplay(bp, "Summary")).ToArray();

            var list = Shape.List();
            list.AddRange(searchShapes);

            var searchShape = Shape.Parts_SearchResults(
                ContentItems: list, Pager: Shape.Pager(pager).TotalItemCount(searchCount));

            return new ShapeResult(this, searchShape);
        }

        public JsonResult Find(string snippet)
        {
            return
                this.Json(
                    this.searchService.SearchBySnippet(snippet).Select(
                        x =>
                        new { x.Id, x.As<ITitleAspect>().Title, Path = string.Concat("/", x.As<IAliasAspect>().Path) }),
                    JsonRequestBehavior.AllowGet);
        }
    }
}