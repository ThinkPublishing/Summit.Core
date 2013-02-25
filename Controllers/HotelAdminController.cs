// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HotelAdminController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   controller for hotel admin
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Orchard.Mvc;

namespace Summit.Core.Controllers
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;
    using Orchard.Core.Contents.Settings;
    using Orchard.Localization;
    using Orchard.Mvc.AntiForgery;
    using Orchard.Mvc.Extensions;
    using Orchard.UI.Admin;
    using Orchard.UI.Notify;

    using Summit.Core.Extensions;
    using Summit.Core.Models;
    using Summit.Core.Services;

    /// <summary>
    /// TODO: (PH:Autoroute) This replicates a whole lot of Core.Contents functionality. All we actually need to do is take the BlogId from the query string in the BlogPostPartDriver, and remove
    /// helper extensions from UrlHelperExtensions.
    /// </summary>
    [ValidateInput(false)]
    [Admin]
    public class HotelAdminController : Controller, IUpdateModel
    {
        private readonly IOrchardServices orchardServices;
        private readonly IDestinationService destinationService;
        private readonly IHotelService hotelService;

        public HotelAdminController(
            IOrchardServices services, IOrchardServices orchardServices, IDestinationService destinationService, IHotelService hotelService)
        {
            Services = services;
            this.orchardServices = orchardServices;
            this.destinationService = destinationService;
            this.hotelService = hotelService;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        public ActionResult Create(int destinationId, int? conceirgeId, bool noConceirge = false)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest).As<DestinationPart>();

            if (destination == null) return HttpNotFound();

            if (!conceirgeId.HasValue && !noConceirge)
            {
                return new ShapeResult(this, this.orchardServices.New.Parts_ConceirgePrompt(Destination: destination));
            }

            var hotel = this.Services.ContentManager.New<HotelPart>("Hotel");
            hotel.DestinationPart = destination;

            if (conceirgeId.HasValue) {
                hotel.SetFieldValue("Conceirge", "", "{" + conceirgeId.Value + "}");
            }

            if (!Services.Authorizer.Authorize(Permissions.EditHotel, destination, T("Not allowed to create a hotel"))) return new HttpUnauthorizedResult();

            dynamic model = Services.ContentManager.BuildEditor(hotel);

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost]
        [ActionName("Create")]
        [FormValueRequired("submit.Save")]
        public ActionResult CreatePOST(int destinationId)
        {
            return CreatePOST(destinationId, false);
        }

        [HttpPost]
        [ActionName("Create")]
        [FormValueRequired("submit.Publish")]
        public ActionResult CreateAndPublishPOST(int destinationId)
        {
            if (!Services.Authorizer.Authorize(Permissions.EditOwnHotel, T("Couldn't create content"))) return new HttpUnauthorizedResult();

            return CreatePOST(destinationId, true);
        }

        private ActionResult CreatePOST(int destinationId, bool publish = false)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest).As<DestinationPart>();

            if (destination == null) return HttpNotFound();

            var hotel = this.Services.ContentManager.New<HotelPart>("Hotel");
            hotel.DestinationPart = destination;

            if (!Services.Authorizer.Authorize(Permissions.EditOwnHotel, destination, T("Couldn't create hotelt"))) return new HttpUnauthorizedResult();

            this.Services.ContentManager.Create(hotel, VersionOptions.Draft);
            var model = Services.ContentManager.UpdateEditor(hotel, this);

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            if (publish)
            {
                if (
                    !Services.Authorizer.Authorize(
                        Permissions.PublishHotel, destination.ContentItem, T("Couldn't publish hotel"))) return new HttpUnauthorizedResult();

                Services.ContentManager.Publish(hotel.ContentItem);
            }

            Services.Notifier.Information(T("Your {0} has been created.", hotel.TypeDefinition.DisplayName));
            return Redirect(Url.HotelEdit(hotel));
        }

        //todo: the content shape template has extra bits that the core contents module does not (remove draft functionality)
        //todo: - move this extra functionality there or somewhere else that's appropriate?
        public ActionResult Edit(int destinationId, int hotelId)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest);
            if (destination == null) return HttpNotFound();

            var hotel = hotelService.Get(hotelId, VersionOptions.Latest);
            if (hotel == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.EditHotel, hotel, T("Couldn't edit blog post"))) return new HttpUnauthorizedResult();

            dynamic model = Services.ContentManager.BuildEditor(hotel);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditPOST(int destinationId, int hotelId, string returnUrl)
        {
            return EditPOST(
                destinationId,
                hotelId,
                returnUrl,
                contentItem =>
                    {
                        if (!contentItem.Has<IPublishingControlAspect>()
                            && !contentItem.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable) this.Services.ContentManager.Publish(contentItem);
                    });
        }

        [HttpPost]
        [ActionName("Edit")]
        [FormValueRequired("submit.Publish")]
        public ActionResult EditAndPublishPOST(int destinationId, int hotelId, string returnUrl)
        {
            var blog = destinationService.Get(destinationId, VersionOptions.Latest);
            if (blog == null) return HttpNotFound();

            // Get draft (create a new version if needed)
            var blogPost = hotelService.Get(hotelId, VersionOptions.DraftRequired);
            if (blogPost == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.PublishHotel, blogPost, T("Couldn't publish hotel"))) return new HttpUnauthorizedResult();

            return EditPOST(
                destinationId, hotelId, returnUrl, contentItem => this.Services.ContentManager.Publish(contentItem));
        }

        public ActionResult EditPOST(
            int destinationId, int hotelId, string returnUrl, Action<ContentItem> conditionallyPublish)
        {
            var blog = destinationService.Get(destinationId, VersionOptions.Latest);
            if (blog == null) return HttpNotFound();

            // Get draft (create a new version if needed)
            var hotel = hotelService.Get(hotelId, VersionOptions.DraftRequired);
            if (hotel == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.EditHotel, hotel, T("Couldn't edit hotel"))) return new HttpUnauthorizedResult();

            // Validate form input
            var model = Services.ContentManager.UpdateEditor(hotel, this);
            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
                return View((object)model);
            }

            conditionallyPublish(hotel.ContentItem);

            Services.Notifier.Information(T("Your {0} has been saved.", hotel.TypeDefinition.DisplayName));

            return this.RedirectLocal(returnUrl, Url.HotelEdit(hotel));
        }

        [ValidateAntiForgeryTokenOrchard]
        public ActionResult DiscardDraft(int id)
        {
            // get the current draft version
            var draft = Services.ContentManager.Get(id, VersionOptions.Draft);
            if (draft == null)
            {
                this.Services.Notifier.Information(T("There is no draft to discard."));
                return RedirectToEdit(id);
            }

            // check edit permission
            if (!Services.Authorizer.Authorize(Permissions.EditHotel, draft, T("Couldn't discard hotel draft"))) return new HttpUnauthorizedResult();

            // locate the published revision to revert onto
            var published = this.Services.ContentManager.Get(id, VersionOptions.Published);
            if (published == null)
            {
                Services.Notifier.Information(T("Can not discard draft on unpublished hotel."));
                return RedirectToEdit(draft);
            }

            // marking the previously published version as the latest
            // has the effect of discarding the draft but keeping the history
            draft.VersionRecord.Latest = false;
            published.VersionRecord.Latest = true;

            Services.Notifier.Information(T("hotel draft version discarded"));
            return RedirectToEdit(published);
        }

        private ActionResult RedirectToEdit(int id)
        {
            return RedirectToEdit(this.Services.ContentManager.GetLatest<HotelPart>(id));
        }

        private ActionResult RedirectToEdit(IContent item)
        {
            if (item == null || item.As<HotelPart>() == null) return HttpNotFound();
            return RedirectToAction(
                "Edit", new { destinationId = item.As<HotelPart>().DestinationPart.Id, hotelId = item.ContentItem.Id });
        }

        [ValidateAntiForgeryTokenOrchard]
        public ActionResult Delete(int destinationId, int hotelId)
        {
            //refactoring: test PublishBlogPost/PublishBlogPost in addition if published

            var destination = destinationService.Get(destinationId, VersionOptions.Latest);
            if (destination == null) return HttpNotFound();

            var hotel = hotelService.Get(hotelId, VersionOptions.Latest);
            if (hotel == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.DeleteHotel, hotel, T("Couldn't delete hotel"))) return new HttpUnauthorizedResult();

            hotelService.Delete(hotel);
            Services.Notifier.Information(T("Hotel was successfully deleted"));

            return Redirect(Url.DestinationForAdmin(destination.As<DestinationPart>()));
        }

        [ValidateAntiForgeryTokenOrchard]
        public ActionResult Publish(int destinationId, int hotelId)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest);
            if (destination == null) return HttpNotFound();

            var hotel = hotelService.Get(hotelId, VersionOptions.Latest);
            if (hotel == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.PublishHotel, hotel, T("Couldn't publish hotel"))) return new HttpUnauthorizedResult();

            hotelService.Publish(hotel);
            Services.Notifier.Information(T("hotel successfully published."));

            return Redirect(Url.DestinationForAdmin(destination.As<DestinationPart>()));
        }

        [ValidateAntiForgeryTokenOrchard]
        public ActionResult Unpublish(int destinationId, int hotelId)
        {
            var destination = destinationService.Get(destinationId, VersionOptions.Latest);
            if (destination == null) return HttpNotFound();

            var hotel = hotelService.Get(hotelId, VersionOptions.Latest);
            if (hotel == null) return HttpNotFound();

            if (!Services.Authorizer.Authorize(Permissions.PublishHotel, hotel, T("Couldn't unpublish hotel"))) return new HttpUnauthorizedResult();

            hotelService.Unpublish(hotel);
            Services.Notifier.Information(T("Hotel successfully unpublished."));

            return Redirect(Url.DestinationForAdmin(destination.As<DestinationPart>()));
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

    public class FormValueRequiredAttribute : ActionMethodSelectorAttribute
    {
        private readonly string _submitButtonName;

        public FormValueRequiredAttribute(string submitButtonName)
        {
            _submitButtonName = submitButtonName;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var value = controllerContext.HttpContext.Request.Form[_submitButtonName];
            return !string.IsNullOrEmpty(value);
        }
    }
}