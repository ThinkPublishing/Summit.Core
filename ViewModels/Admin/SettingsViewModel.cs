// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels.Admin
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Summit.Core.Models;

    public class SettingsViewModel
    {
        public SettingsViewModel(){}

        public SettingsViewModel(ImagePowerToolsSettingsRecord settingsRecord)
        {
            EnableFrontendResizeAction = settingsRecord.EnableFrontendResizeAction;
            MaxImageWidth = settingsRecord.MaxImageWidth;
            MaxImageHeight = settingsRecord.MaxImageHeight;
        }

        [Required]
        public bool EnableFrontendResizeAction { get; set; }
        
        [Required]
        [DisplayName("Maximal Image Width")]
        public int MaxImageWidth { get; set; }
        [Required]
        [DisplayName("Maximal Image Height")]
        public int MaxImageHeight { get; set; }

    }
}