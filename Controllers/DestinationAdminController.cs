// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationAdminController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   destination admin controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.Data;
    using Orchard.DisplayManagement;
    using Orchard.Localization;
    using Orchard.Settings;
    using Orchard.UI.Admin;
    using Orchard.UI.Navigation;
    using Orchard.UI.Notify;

    using Summit.Core.Extensions;
    using Summit.Core.Models;
    using Summit.Core.Services;

    [ValidateInput(false)]
    [Admin]
    public class DestinationAdminController : Controller, IUpdateModel
    {
        private readonly IDestinationService destinationService;

        private readonly IHotelService hotelService;

        private readonly IContentManager contentManager;

        private readonly ITransactionManager transactionManager;

        private readonly ISiteService siteService;

        public DestinationAdminController(
            IOrchardServices services,
            IDestinationService destinationService,
            IHotelService hotelService,
            IContentManager contentManager,
            ITransactionManager transactionManager,
            ISiteService siteService,
            IShapeFactory shapeFactory)
        {
            Services = services;
            this.destinationService = destinationService;
            this.hotelService = hotelService;
            this.contentManager = contentManager;
            this.transactionManager = transactionManager;
            this.siteService = siteService;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        private dynamic Shape { get; set; }

        public Localizer T { get; set; }

        public IOrchardServices Services { get; set; }

        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageDestinations, T("Not allowed to create destinations"))) return new HttpUnauthorizedResult();

            var destination = this.Services.ContentManager.New<DestinationPart>("Destination");
            if (destination == null) return HttpNotFound();

            dynamic model = this.Services.ContentManager.BuildEditor(destination);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePOST()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageDestinations, T("Couldn't create destination"))) return new HttpUnauthorizedResult();

            var destination = this.Services.ContentManager.New<DestinationPart>("Destination");

            this.contentManager.Create(destination, VersionOptions.Draft);
            dynamic model = this.contentManager.UpdateEditor(destination, this);

            if (!ModelState.IsValid)
            {
                transactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            contentManager.Publish(destination.ContentItem);
            return Redirect(Url.DestinationForAdmin(destination));
        }

        public ActionResult Edit(int destinationId)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest);

            if (
                !Services.Authorizer.Authorize(
                    Permissions.ManageDestinations, destination, T("Not allowed to edit destination"))) return new HttpUnauthorizedResult();

            if (destination == null) return HttpNotFound();

            dynamic model = Services.ContentManager.BuildEditor(destination);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Delete")]
        public ActionResult EditDeletePOST(int destinationId)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageDestinations, T("Couldn't delete destination"))) return new HttpUnauthorizedResult();

            var destination = destinationService.Get(destinationId, VersionOptions.DraftRequired);
            if (destination == null) return HttpNotFound();

            this.destinationService.Delete(destination);

            this.Services.Notifier.Information(T("destination deleted"));

            return Redirect(Url.DestinationsForAdmin());
        }


        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditPOST(int destinationId)
        {
            var destination = this.destinationService.Get(destinationId, VersionOptions.DraftRequired);

            if (!Services.Authorizer.Authorize(Permissions.ManageDestinations, destination, T("Couldn't edit destination"))) return new HttpUnauthorizedResult();

            if (destination == null) return HttpNotFound();

            dynamic model = this.Services.ContentManager.UpdateEditor(destination, this);
            if (!ModelState.IsValid)
            {
                this.Services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            this.contentManager.Publish(destination);
            this.Services.Notifier.Information(T("Destination information updated"));

            return Redirect(Url.DestinationsForAdmin());
        }

        [HttpPost]
        public ActionResult Remove(int destinationId)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageDestinations, T("Couldn't delete destination"))) return new HttpUnauthorizedResult();

            var destination = destinationService.Get(destinationId, VersionOptions.Latest);

            if (destination == null) return HttpNotFound();

            destinationService.Delete(destination);

            Services.Notifier.Information(T("Destination was successfully deleted"));
            return Redirect(Url.DestinationsForAdmin());
        }

        public ActionResult List()
        {
            var list = this.Services.New.List();
            list.AddRange(
                destinationService.Get(VersionOptions.Latest).Select(
                    b =>
                        {
                            var destination = this.Services.ContentManager.BuildDisplay(b, "SummaryAdmin");
                            destination.TotalPostCount = this.hotelService.HotelCount(b, VersionOptions.Latest);
                            return destination;
                        }));

            dynamic viewModel = this.Services.New.ViewModel().ContentItems(list);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

        public ActionResult Item(int destinationId, PagerParameters pagerParameters)
        {
            var pager = new Pager(this.siteService.GetSiteSettings(), pagerParameters);
            var destinationPart = this.destinationService.Get(destinationId, VersionOptions.Latest).As<DestinationPart>();

            if (destinationPart == null) return HttpNotFound();

            var hotels =
                this.hotelService.Get(destinationPart, pager.GetStartIndex(), pager.PageSize, VersionOptions.Latest).ToArray();
            var hotelsShapes = hotels.Select(bp => this.contentManager.BuildDisplay(bp, "SummaryAdmin")).ToArray();

            dynamic destinationShape = Services.ContentManager.BuildDisplay(destinationPart, "DetailAdmin");

            var list = Shape.List();
            list.AddRange(hotelsShapes);
            destinationShape.Content.Add(Shape.Parts_Destinations_Hotel_ListAdmin(ContentItems: list), "5");

            var totalItemCount = this.hotelService.HotelCount(destinationPart, VersionOptions.Latest);
            destinationShape.Content.Add(Shape.Pager(pager).TotalItemCount(totalItemCount), "Content:after");

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)destinationShape);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(
            TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}