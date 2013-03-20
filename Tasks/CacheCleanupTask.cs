// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheCleanupTask.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Tasks
{
    using System;

    using Orchard.Logging;
    using Orchard.Tasks;

    using Summit.Core.Services;

    public class CacheCleanupTask : IBackgroundTask
    {
        private static bool _wasStarted = false;
        private static readonly object _syncRoot = new object();
        private static DateTime _lastRun = new DateTime(2010, 10, 10);

        private const int CleanPeriodInMinutes = 30;
        private IPowerToolsSettingsService _settingsService;

        public ILogger Logger { get; set; }

        public CacheCleanupTask(IPowerToolsSettingsService settingsService)
        {
            Logger = NullLogger.Instance;
            _settingsService = settingsService;
        }

        public void Sweep()
        {
            if (_wasStarted)
                return; 
            lock (_syncRoot)
            {
                if (_lastRun > DateTime.Now.AddMinutes(CleanPeriodInMinutes * -1))
                {
                    return;
                }
                _wasStarted = true;
                try
                {
                    var resizeService = new ImageResizerService(_settingsService);
                    resizeService.DeleteOldCache();
                    _lastRun = DateTime.Now;
                    _settingsService.Settings.DeleteOldLastJobRun = _lastRun;
                    _settingsService.SaveSettings();
                }
                catch(Exception e)
                {
                     Logger.Error(e, "Summit.Core Cache cleanup task failed");   
                }
                finally
                {
                    _wasStarted = false;
                }
            }
        }
    }
}