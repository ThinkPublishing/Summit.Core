using Orchard.UI.Resources;

namespace Summit.Core {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineScript("GoogleMapsV3").SetCdn("http://maps.google.com/maps/api/js?sensor=true");
            manifest.DefineScript("gmap3").SetUrl("gmap3.min.js").SetDependencies(new [] { "jQuery", "GoogleMapsV3" });
        }
    }
}
