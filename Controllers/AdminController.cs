// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminController.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   image tools admin controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Controllers
{
    using System.Web.Mvc;

    using Orchard.UI.Admin;

    using Summit.Core.Services;
    using Summit.Core.ViewModels.Admin;

    [Admin]
    public class AdminController : Controller
    {
        private readonly IPowerToolsSettingsService settingsService;
        private readonly IImageResizerService resizerService;

        public AdminController(IPowerToolsSettingsService settingsService, IImageResizerService resizerService)
        {
            this.settingsService = settingsService;
            this.resizerService = resizerService;
        }

        public ActionResult Settings()
        {
            var viewModel = new SettingsViewModel(this.settingsService.Settings);
            return View(viewModel);
        }

        public ActionResult Cache()
        {
            var viewModel = new CacheStatisticsViewModel(this.settingsService.Settings);
            this.resizerService.CacheStatistics(out viewModel.FilesCount, out viewModel.TotalSize);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Settings(SettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            this.settingsService.Settings.EnableFrontendResizeAction = viewModel.EnableFrontendResizeAction;
            this.settingsService.Settings.MaxImageHeight = viewModel.MaxImageHeight;
            this.settingsService.Settings.MaxImageWidth = viewModel.MaxImageWidth;
            this.settingsService.SaveSettings();
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ClearCache()
        {
            this.resizerService.ClearCache();
            return RedirectToAction("Cache");
        }
    }
}