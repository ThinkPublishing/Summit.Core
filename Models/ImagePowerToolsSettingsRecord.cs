﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImagePowerToolsSettingsRecord.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models
{
    using System;

    public class ImagePowerToolsSettingsRecord
    {
        public virtual int Id { get; set; }
        public virtual int MaxCacheSizeMB { get; set; }
        public virtual int MaxCacheAgeDays { get; set; }
        public virtual bool EnableFrontendResizeAction { get; set; }
        public virtual int MaxImageWidth { get; set; }
        public virtual int MaxImageHeight { get; set; }
        public virtual DateTime? DeleteOldLastJobRun { get; set; }
    }
}