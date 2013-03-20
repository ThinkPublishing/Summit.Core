// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConceirgeAdminController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   Conceirge admin
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers 
{
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
    public class ConciergeAdminController : Controller, IUpdateModel {
        private readonly IDestinationService destinationService;
        private readonly IContentManager contentManager;
        private readonly ITransactionManager transactionManager;
        private readonly ISiteService siteService;

        public ConciergeAdminController(
            IOrchardServices services,
            IDestinationService destinationService,
            IContentManager contentManager,
            ITransactionManager transactionManager,
            ISiteService siteService,
            IShapeFactory shapeFactory) {
            Services = services;
            this.destinationService = destinationService;
            this.contentManager = contentManager;
            this.transactionManager = transactionManager;
            this.siteService = siteService;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        private dynamic Shape { get; set; }

        public Localizer T { get; set; }

        public IOrchardServices Services { get; set; }

        public ActionResult Create(int? destinationId) {
            if (!Services.Authorizer.Authorize(Permissions.EditHotel, T("Not allowed to create create concierges"))) return new HttpUnauthorizedResult();

            var conc = this.Services.ContentManager.New("Concierge").As("ConciergePart");
            if (conc == null) return HttpNotFound();

            dynamic model = this.Services.ContentManager.BuildEditor(conc);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object) model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePOST(int? destinationId) {
            if (!Services.Authorizer.Authorize(Permissions.EditHotel, T("Couldn't create concierge"))) return new HttpUnauthorizedResult();

            var conc = this.Services.ContentManager.New("Concierge").As("ConciergePart");

            this.contentManager.Create(conc, VersionOptions.Draft);
            dynamic model = this.contentManager.UpdateEditor(conc, this);

            if (!ModelState.IsValid) {
                transactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object) model);
            }

            contentManager.Publish(conc.ContentItem);

            var destinationPart = destinationId.HasValue ? destinationService.Get(destinationId.Value, VersionOptions.Latest) : null;
            return destinationPart != null ? Redirect(Url.HotelCreate(destinationPart.As<DestinationPart>(), conc.Id)) : Redirect(Url.DestinationCreate());
        }

        public ActionResult Edit(int conciergeId) {
            var conc = this.contentManager.Get(conciergeId, VersionOptions.Latest);

            if (
                !Services.Authorizer.Authorize(
                    Permissions.EditHotel, conc, T("Not allowed to edit concierge"))) return new HttpUnauthorizedResult();

            if (conc == null) return HttpNotFound();

            dynamic model = Services.ContentManager.BuildEditor(conc.As("ConciergePart"));
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object) model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Delete")]
        public ActionResult EditDeletePOST(int conciergeId) {
            if (!Services.Authorizer.Authorize(Permissions.EditHotel, T("Couldn't delete concierge"))) return new HttpUnauthorizedResult();

            var conc = destinationService.Get(conciergeId, VersionOptions.DraftRequired);
            if (conc == null) return HttpNotFound();

            this.destinationService.Delete(conc);

            this.Services.Notifier.Information(T("concierge deleted"));

            return Redirect(Url.DestinationsForAdmin());
        }


        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditPOST(int conciergeId) {
            var conc = this.contentManager.Get(conciergeId, VersionOptions.DraftRequired);

            if (!Services.Authorizer.Authorize(Permissions.EditHotel, conc, T("Couldn't edit concierge"))) return new HttpUnauthorizedResult();

            if (conc == null) return HttpNotFound();

            dynamic model = this.Services.ContentManager.UpdateEditor(conc, this);
            if (!ModelState.IsValid) {
                this.Services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object) model);
            }

            this.contentManager.Publish(conc);
            this.Services.Notifier.Information(T("Concierge information updated"));

            return Redirect(Url.DestinationsForAdmin());
        }

        [HttpPost]
        public ActionResult Remove(int conciergeId) {
            if (!Services.Authorizer.Authorize(Permissions.EditHotel, T("Couldn't delete concierge"))) return new HttpUnauthorizedResult();

            var conc = destinationService.Get(conciergeId, VersionOptions.Latest);

            if (conc == null) return HttpNotFound();

            destinationService.Delete(conc);

            Services.Notifier.Information(T("Concierge was successfully deleted"));
            return Redirect(Url.DestinationsForAdmin());
        }

        public ActionResult List() {
            //var list = this.Services.New.List();
            //list.AddRange(
            //    destinationService.Get(VersionOptions.Latest).Select(
            //        b => {
            //            var destination = this.Services.ContentManager.BuildDisplay(b, "SummaryAdmin");
            //            destination.TotalPostCount = this.hotelService.HotelCount(b, VersionOptions.Latest);
            //            return destination;
            //        }));

            //dynamic viewModel = this.Services.New.ViewModel().ContentItems(list);
            //// Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            //return View((object) viewModel);
            return null;
        }

        public ActionResult Item(int destinationId, PagerParameters pagerParameters) {
            return null;
            //var pager = new Pager(this.siteService.GetSiteSettings(), pagerParameters);
            //var destinationPart = this.destinationService.Get(destinationId, VersionOptions.Latest).As<DestinationPart>();

            //if (destinationPart == null) return HttpNotFound();

            //var hotels =
            //    this.hotelService.Get(destinationPart, pager.GetStartIndex(), pager.PageSize, VersionOptions.Latest).ToArray();
            //var hotelsShapes = hotels.Select(bp => this.contentManager.BuildDisplay(bp, "SummaryAdmin")).ToArray();

            //dynamic destinationShape = Services.ContentManager.BuildDisplay(destinationPart, "DetailAdmin");

            //var list = Shape.List();
            //list.AddRange(hotelsShapes);
            //destinationShape.Content.Add(Shape.Parts_Destinations_Hotel_ListAdmin(ContentItems: list), "5");

            //var totalItemCount = this.hotelService.HotelCount(destinationPart, VersionOptions.Latest);
            //destinationShape.Content.Add(Shape.Pager(pager).TotalItemCount(totalItemCount), "Content:after");

            //// Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            //return View((object) destinationShape);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(
            TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}