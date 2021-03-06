// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceManifest.cs" company="Zaust">
//   Copyright (�)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core {
    using Orchard.UI.Resources;

    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("SummitAdmin").SetUrl("summit-admin.css");

            // map stuff
            manifest.DefineScript("GoogleMapsV3").SetCdn("http://maps.google.com/maps/api/js?sensor=true");
            manifest.DefineScript("gmap3").SetUrl("gmap3.min.js").SetDependencies(new [] { "jQuery", "GoogleMapsV3" });

            //search stuff
            manifest.DefineScript("search-widget").SetUrl("search-widget.js").SetDependencies(new[] { "jQuery", "jQueryUI" });

            // core common js
            manifest.DefineScript("summit-core").SetUrl("summit-core").SetDependencies(new[] { "jQuery" });
        }
    }
}
