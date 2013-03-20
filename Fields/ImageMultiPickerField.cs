// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageMultiPickerField.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Fields
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Orchard.ContentManagement;

    using Summit.Core.Models;

    public class ImageMultiPickerField : ContentField
    {
        private IEnumerable<SelectedImage> _dataCache = null;

        public string Data
        {
            get
            {
                return Storage.Get<string>("Data");
            }
            set
            {
                _dataCache = null;
                Storage.Set("Data", value);
            }
        }

        public IEnumerable<SelectedImage> Images
        {
            get
            {
                if (_dataCache != null) return _dataCache;
                var data = Data;
                if (string.IsNullOrEmpty(data)) return new List<SelectedImage>();
                _dataCache = JsonConvert.DeserializeObject<IEnumerable<SelectedImage>>(data);
                return _dataCache;
            }
            set
            {
                _dataCache = null;
                Data = JsonConvert.SerializeObject(value);
            }
        }
    }
}