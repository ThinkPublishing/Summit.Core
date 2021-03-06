﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageResizerService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    using ImageResizer;
    using ImageResizer.Configuration;

    using Orchard;
    using Orchard.Logging;

    using Summit.Core.Extensions;
    using Summit.Core.ImageResizerFilters;

    public interface IImageResizerService : IDependency
    {
        string GetCleanFileExtension(string url);
        string GetMimeTypeByImageExtesion(string fileExtention);
        IEnumerable<string> SupportedFileExtensions();
        bool IsSupportedNonImage(string fileName);
        bool IsImage(string fileName);

        string ResizeImage(string url, int width = 0, int height = 0, int maxWidth = 0, int maxHeight = 0, int quality = 0, string settings = "");

        string ResizeImage(string url, string settings);

        void ClearCache();
        void CacheStatistics(out long fileCount, out long totalSize);
    }

    public class ImageResizerService : IImageResizerService
    {
        public ILogger Logger { get; set; }
        private IPowerToolsSettingsService _settingsService;

        public ImageResizerService(IPowerToolsSettingsService settingsService)
        {
            _settingsService = settingsService;
            Logger = NullLogger.Instance;
        }

        public bool IsSupportedNonImage(string fileName)
        {
            return fileName.IsMatch(@"\.swf$");
        }

        public bool IsImage(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            if (ext != null)
            {
                var extension = ext.Trim('.');
                return ImageBuilder.Current.GetSupportedFileExtensions().Any(x => x == extension);
            }
            return false;
        }

        private string CreateMd5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string GetMimeTypeByImageExtesion(string fileExtention)
        {
            string retVal = ImageTypes.Jpg;
            switch (fileExtention.ToLower())
            {
                case "bmp":
                    retVal = ImageTypes.Bmp;
                    break;
                case "jpeg":
                    retVal = ImageTypes.Jpg;
                    break;
                case "png":
                    retVal = ImageTypes.Png;
                    break;
                case "gif":
                    retVal = ImageTypes.Gif;
                    break;
                case "exif":
                    retVal = ImageTypes.Exif;
                    break;
                case "tif":
                    retVal = ImageTypes.Tif;
                    break;
                case "tiff":
                    retVal = ImageTypes.Tiff;
                    break;
                case "tff":
                    retVal = ImageTypes.Tff;
                    break;
                case "jpe":
                    retVal = ImageTypes.Jpe;
                    break;
                case "jif":
                    retVal = ImageTypes.Jif;
                    break;
                case "jfif":
                    retVal = ImageTypes.Jfif;
                    break;
                case "jfi":
                    retVal = ImageTypes.Jfi;
                    break;
            }
            return retVal;
        }

        public IEnumerable<string> SupportedFileExtensions()
        {
            return ImageBuilder.Current.GetSupportedFileExtensions();
        }

        public string ResizeImage(
            string url, int width = 0, int height = 0, int maxWidth = 0, int maxHeight = 0,  
            int quality = 0,
            string settings = "")
        {
            var resizeSettings = new ResizeSettings(settings);
            if (quality != 0)
                resizeSettings.Quality = quality;
            if (width > 0)
                resizeSettings.Width = width;
            if (height > 0)
                resizeSettings.Height = height;
            if (maxHeight > 0)
                resizeSettings.MaxHeight = maxHeight;
            if (maxWidth > 0)
                resizeSettings.MaxWidth = maxWidth;
            
            return GetResizedUrl(url, resizeSettings);
        }

        public string ResizeImage(string url, string settings)
        {
            return GetResizedUrl(url, new ResizeSettings(settings));
        }

        private bool IsResizeSettingsValid(ResizeSettings settings)
        {
            if (_settingsService == null)
                return true;
            return
                settings.Width < _settingsService.Settings.MaxImageWidth &&
                settings.MaxWidth < _settingsService.Settings.MaxImageWidth &&
                settings.Height < _settingsService.Settings.MaxImageHeight &&
                settings.MaxHeight < _settingsService.Settings.MaxImageHeight;
        }

        private string GetResizedUrl(string url, ResizeSettings settings)
        {
            try
            {
                if (settings.WasOneSpecified(GrayscaleFilter.FilterKey))
                {
                    Config.Current.Plugins.GetOrInstall<GrayscaleFilter>();
                }

                if (!url.StartsWith("/"))
                {
                    url = "/" + url;
                }

                if (!IsResizeSettingsValid(settings))
                {
                    return "";
                }

                var imageServerPath = GetServerPath(url);
                if (!File.Exists(imageServerPath))
                {
                    return "";
                }

                imageServerPath = FilterUnsupportedFiles(imageServerPath);
                if (string.IsNullOrWhiteSpace(imageServerPath)) 
                    return "";

                var cachedImagePath = GetImageCachePath(url, settings);
                string cachedImageServerPath = GetServerPath(cachedImagePath);

                if (!File.Exists(cachedImageServerPath))
                {
                    WriteResizedImage(cachedImageServerPath, imageServerPath, settings);
                }
                else
                {
                    var cacheFileInfo = new FileInfo(cachedImageServerPath);
                    var imageFileInfo = new FileInfo(imageServerPath);
                    if (cacheFileInfo.LastWriteTimeUtc < imageFileInfo.LastWriteTimeUtc)
                    {
                        WriteResizedImage(cachedImageServerPath, imageServerPath, settings);
                    }
                }
                return cachedImagePath;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string FilterUnsupportedFiles(string imageServerPath)
        {
            string ext = GetCleanFileExtension(imageServerPath);
            if (!SupportedFileExtensions().Contains(ext))
            {
                var alternativeUrl = GetServerPath(ImageConsts.ModuleContentFolder + ext + ".png");
                if (File.Exists(alternativeUrl))
                {
                    imageServerPath = alternativeUrl;
                }
                else
                {
                    return GetServerPath(ImageConsts.ModuleContentFolder + "file.png");
                }
            }
            return imageServerPath;
        }


        private static void WriteResizedImage(string cachedImageServerPath, string imageServerPath, ResizeSettings settings)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    WriteResizedFile(cachedImageServerPath, imageServerPath, settings);
                    break;
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }

        private static void WriteResizedFile(string cachedImageServerPath, string imageServerPath, ResizeSettings settings)
        {
            string cachedImageDir = Path.GetDirectoryName(cachedImageServerPath);
            if (!string.IsNullOrWhiteSpace(cachedImageDir) && !Directory.Exists(cachedImageServerPath))
            {
                Directory.CreateDirectory(cachedImageDir);
            }
            ImageBuilder.Current.Build(imageServerPath, cachedImageServerPath, settings);
        }

        public string GetImageCachePath(string url, ResizeSettings settings)
        {
            string imageDir = Path.GetDirectoryName(url);
            if (imageDir == null)
            {
                imageDir = string.Empty;
            }
            string hasedProperties = CreateMd5Hash(settings.ToString());
            string cachedFileName = string.Format("{0}/{1}-{2}.{3}",
                GetFolderHash(imageDir),
                Path.GetFileNameWithoutExtension(url), 
                hasedProperties,
                GetCleanFileExtension(url));
            string cachedImagePath = string.Concat(ImageConsts.CacheFolderPath, "/", cachedFileName.TrimStart('/', '\\'));
            return cachedImagePath;
        }

        private string GetFolderHash(string imageDir)
        {
            return imageDir
                .Trim('/', '\\', ' ')
                .Replace('\\', '/')
                .RegexRemove("^Media");
        }

        public static string GetServerPath(string cachedImagePath)
        {   
            if (!cachedImagePath.StartsWith("~/"))
                cachedImagePath = "~/" + cachedImagePath;
            return System.Web.Hosting.HostingEnvironment.MapPath(cachedImagePath);
        }

        public string GetCleanFileExtension(string url)
        {
            var extension = Path.GetExtension(url);
            if (extension != null)
            {
                return extension.Replace(".", "").ToLower();
            }
            return string.Empty;
        }

        
        internal void DeleteOldCache()
        {
            var mediaFolder = GetServerPath("/Media");
            var cacheFolder = GetServerPath(ImageConsts.CacheFolderPath);
            var cacheMediaFolders = Directory
                .GetDirectories(cacheFolder, "*", SearchOption.AllDirectories)
                .OrderBy(x => x.Length);

            var fileNameRegex = new Regex(@"^(.+)-[^-]+\.(\w+)$", RegexOptions.Compiled);
            foreach (var cacheMediaFolder in cacheMediaFolders)
            {
                if (cacheMediaFolder.Length < cacheFolder.Length)
                    continue;
                var relativePath = cacheMediaFolder
                    .Substring(cacheFolder.Length)
                    .Trim('/', '\\');
                var mediaPath = Path.Combine(mediaFolder, relativePath);
                if (!Directory.Exists(mediaPath))
                {
                    try
                    {
                        Directory.Delete(cacheMediaFolder, true);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e, "Cannot delete folder " + cacheMediaFolder);
                    }
                }
                else
                {
                    var mediaFiles = new HashSet<string>(
                        Directory.GetFiles(mediaPath)
                            .Select(x => x.ToLower())
                            .Distinct()
                            .ToList());
                    var cachedFiles = Directory.GetFiles(cacheMediaFolder).Select(x => x.ToLower()).ToList();
                    foreach (var cachedFilePath in cachedFiles)
                    {
                        var cachedfileName = Path.GetFileName(cachedFilePath);
                        if(string.IsNullOrWhiteSpace(cachedfileName))
                            continue;
                        var fileNameMatch = fileNameRegex.Match(cachedfileName);
                        if (!fileNameMatch.Success)
                            continue;
                        var fileToCheck = fileNameMatch.Groups[1].Value + "." + fileNameMatch.Groups[2].Value;
                        fileToCheck = Path.Combine(mediaPath, fileToCheck).ToLower();
                        if (!mediaFiles.Contains(fileToCheck))
                        {
                            try
                            {
                                File.Delete(cachedFilePath);
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e, "Cannot delete file " + cachedFilePath);
                            }
                        }
                    }
                }
            }
        }

        public void ClearCache()
        {
            var cacheDir = new DirectoryInfo(GetServerPath(ImageConsts.CacheFolderPath));
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    foreach (var file in cacheDir.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (var dir in cacheDir.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch
                {
                }
                return;                
            }
        }

        public void CacheStatistics(out long fileCount, out long totalSize)
        {
            var cacheDir = new DirectoryInfo(GetServerPath(ImageConsts.CacheFolderPath));
            var allFiles = cacheDir.GetFiles("*.*", SearchOption.AllDirectories);
            fileCount = allFiles.Count();
            if (fileCount == 0)
                totalSize = 0;
            else
                totalSize = allFiles.Sum(x => x.Length);
        }
    }
}