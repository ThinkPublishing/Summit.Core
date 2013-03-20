// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaFolderEditViewModel.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.ViewModels.Multipicker
{
    using System.Collections.Generic;
    using System.Web;

    using Orchard.Media.Models;

    using Summit.Core.ViewModels.Admin;

    public class MediaFolderEditViewModel
    {
        public string SearchFilter { get; set; }
        public string FolderName { get; set; }
        public string MediaPath { get; set; }
        public IEnumerable<MediaFolder> MediaFolders { get; set; }
        public IEnumerable<ImageFileViewModel> MediaFiles { get; set; }
        public string PublicPath { get; set; }

        public IEnumerable<BreadcrumbViewModel> BreadCrumbs { get; set; }

        public string GetPickerUrl(string folderMediaPath)
        {
            var result = string.Format(
                "/Summit.Core/Multipicker/Index?scope={0}&mediaPath={1}",
                Scope,
                HttpUtility.HtmlEncode(folderMediaPath));
            return result;
        }

        public string Scope { get; set; }

        public bool IsFolderNotExists { get; set; }
    }

    public class ImageFileViewModel
    {
        public MediaFile MediaFile { get; set; }
        //public string PublicUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsImage { get; set; }
        public string Extension { get; set; }
    }
}