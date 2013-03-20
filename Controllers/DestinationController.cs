// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   destination controller 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers 
{
    using System.Linq;
    using System.Web.Mvc;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.DisplayManagement;
    using Orchard.Logging;
    using Orchard.Mvc;
    using Orchard.Settings;
    using Orchard.Themes;
    using Orchard.UI.Navigation;

    using Summit.Core.Models;
    using Summit.Core.Routing;
    using Summit.Core.Services;

    [Themed]
    public class DestinationController : Controller {
        private readonly IOrchardServices services;
        private readonly IDestinationService destinationService;
        private readonly IHotelService hotelService;
        private readonly ISiteService siteService;

        public DestinationController(
            IOrchardServices services, 
            IDestinationService destinationService,
            IHotelService hotelService,
            IDestinationPathConstraint destinationPathConstraint, 
            IShapeFactory shapeFactory,
            ISiteService siteService) {
            this.services = services;
            this.destinationService = destinationService;
            this.hotelService = hotelService;
            this.siteService = siteService;
            this.Logger = NullLogger.Instance;
            this.Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        protected ILogger Logger { get; set; }

        public ActionResult List() {
            var destinations = this.destinationService.Get().Select(b => this.services.ContentManager.BuildDisplay(b, "Summary"));

            var list = this.Shape.List();
            list.AddRange(destinations);

            dynamic viewModel = this.Shape.ViewModel().ContentItems(list);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

        public ActionResult Item(int destinationId, PagerParameters pagerParameters) {
            var pager = new Pager(this.siteService.GetSiteSettings(), pagerParameters);

            var destinationPart = this.destinationService.Get(destinationId, VersionOptions.Published).As<DestinationPart>();
            if (destinationPart == null)
                return HttpNotFound();

            var hotelParts = this.hotelService.Get(destinationPart, pager.GetStartIndex(), pager.PageSize);
            var hotels = hotelParts.Select(b => this.services.ContentManager.BuildDisplay(b, "Summary"));
            dynamic destination = this.services.ContentManager.BuildDisplay(destinationPart);

            var list = this.Shape.List();
            list.AddRange(hotels);
            destination.Content.Add(this.Shape.Parts_Destinations_Hotel_List(ContentItems: list, ItemModels: hotelParts), "5");

            var totalItemCount = this.hotelService.HotelCount(destinationPart);
            destination.Content.Add(this.Shape.Pager(pager).TotalItemCount(totalItemCount), "Content:after");

            return new ShapeResult(this, destination);
        }
    }
}
