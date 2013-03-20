// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageResizerController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   resize image controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers
{
    using System.Web.Mvc;

    using ImageResizer.Configuration;

    using Orchard.Mvc.Extensions;
    using Orchard.Themes;

    using Summit.Core.Services;

    [HandleError]
    [Themed(false)]
    public class ImageResizerController : Controller
    {
        private readonly IImageResizerService _imageResizerService;
        private readonly IPowerToolsSettingsService _settingsService; 

        public ImageResizerController(
            IImageResizerService imageResizerService,
            IPowerToolsSettingsService settingsService)
        {
            _imageResizerService = imageResizerService;
            _settingsService = settingsService;
        }

        public ActionResult Test()
        {
            return Content(Config.Current.GetDiagnosticsPage());
        }

        public ActionResult ResizedImage(
            string url, 
            string defaultImage = "/modules/Summit.Core/content/image_not_found.jpg")
        {
            if (!_settingsService.Settings.EnableFrontendResizeAction && !User.Identity.IsAuthenticated)
                return HttpNotFound();
            if (string.IsNullOrWhiteSpace(url))
                return HttpNotFound();

            var retValImageUrl = _imageResizerService.ResizeImage(url, Request.Url.Query);
            if (string.IsNullOrWhiteSpace(retValImageUrl))
            {
                retValImageUrl = _imageResizerService.ResizeImage(defaultImage, Request.Url.Query);  
            }
            if (string.IsNullOrWhiteSpace(retValImageUrl))
            {
                return HttpNotFound();
            }
            return this.RedirectLocal(retValImageUrl);
        }
    }
}