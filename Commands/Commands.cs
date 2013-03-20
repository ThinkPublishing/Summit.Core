// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   Commands file
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Commands
{
    using Orchard.Commands;

    using Summit.Core.Services;

    public class Go2SeeCommands : DefaultOrchardCommandHandler
    {
        private readonly IPowerToolsSettingsService settingsService;
   
        public Go2SeeCommands(
            IPowerToolsSettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        [CommandHelp("Deletes cache for deleted images")]
        [CommandName("amba.ipt deleteold")]
        public void CacheDeleteOld() 
        {
            var resizeService = new ImageResizerService(this.settingsService);
            resizeService.DeleteOldCache();
        }

        [CommandHelp("Deletes all files from cache")]
        [CommandName("amba.ipt clearcache")]
        public void ClearCache()
        {
            var resizeService = new ImageResizerService(this.settingsService);
            resizeService.ClearCache();
        }
    }
}