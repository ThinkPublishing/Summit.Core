// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheStatisticsViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels.Admin
{
    using System;

    using Summit.Core.Models;

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