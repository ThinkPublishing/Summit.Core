// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectedImage.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Models
{
    using Newtonsoft.Json;

    public class SelectedImage
    {
        [JsonProperty(PropertyName = "file")]
        public string FilePath { get; set; }
        [JsonProperty(PropertyName = "descr")]
        public string Description { get; set; }
    }
}