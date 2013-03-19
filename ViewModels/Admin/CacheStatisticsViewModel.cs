using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Summit.Core.Models;

namespace Summit.Core.ViewModels.Admin
{
    public class CacheStatisticsViewModel
    {
        public DateTime? DeleteOldLastJobRun { get; set; }

        public long FilesCount;
        public long TotalSize;

        public CacheStatisticsViewModel(ImagePowerToolsSettingsRecord settingsRecord)
        {
            DeleteOldLastJobRun = settingsRecord.DeleteOldLastJobRun;
        }
    }
}