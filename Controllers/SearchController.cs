// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationAdminController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   destination admin controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Orchard.ContentManagement.Aspects;
using Orchard.Mvc;
using Orchard.Themes;

namespace Summit.Core.Controllers {
    using System.Linq;
    using System.Web.Mvc;
    using Orchard.ContentManagement;
    using Orchard.DisplayManagement;
    using Orchard.Localization;
    using Orchard.Settings;
    using Services;


    public class SearchController : Controller {
        private readonly ISiteService siteService;
        private readonly ISearchService searchService;

        public SearchController(
            ISiteService siteService,
            IShapeFactory shapeFactory, ISearchService searchService) {
            this.siteService = siteService;
            this.searchService = searchService;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        private dynamic Shape { get; set; }
        private Localizer T { get; set; }

        [Themed]
        public ShapeResult Index(string search) {
            var shape = Shape.Parts_SearchResults();
            return new ShapeResult(this, shape);
        }

        public JsonResult Find(string snippet) {
            return this.Json(this.searchService.Search(snippet).Select(x => new { x.Id, x.As<ITitleAspect>().Title, Path = string.Concat("/", x.As<IAliasAspect>().Path) }), JsonRequestBehavior.AllowGet);
        }
    }
}