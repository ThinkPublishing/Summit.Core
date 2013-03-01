using System.Linq;
using Summit.Core.Services;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Summit.Core {
    using Summit.Core.Services;

    public class AdminMenu : INavigationProvider {
        private readonly IDestinationService destinationService;

        public AdminMenu(IDestinationService destinationService) {
            this.destinationService = destinationService;
        }

        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Taxonomies"), "4",
                        menu => menu
                                    .Add(T("Manage Taxonomies"), "1.0", item => item.Action("Index", "TaxonomyAdmin", new { area = "Summit.Core" }).Permission(Permissions.ManageTaxonomies))
                                    );

            builder.AddImageSet("summit")
                .Add(T("Summit"), "1.5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu) {
            var destinations = this.destinationService.Get();
            var destinationCount = destinations.Count();
            var singleDestination = destinationCount == 1 ? destinations.ElementAt(0) : null;

            if (destinationCount > 0 && singleDestination == null) {
                menu.Add(T("Manage Destinations"), "3",
                         item => item.Action("List", "DestinationAdmin", new { area = "Summit.Core" }).Permission(Permissions.MetaListDestinations));
            }
            else if (singleDestination != null)
                menu.Add(T("Manage Destination"), "1.0",
                    item => item.Action("Item", "DestinationAdmin", new { area = "Summit.Core", destinationId = singleDestination.Id }).Permission(Permissions.MetaListOwnDestinations));

            if (singleDestination != null)
                menu.Add(T("New Hotel"), "1.1",
                         item =>
                         item.Action("Create", "HotelAdmin", new { area = "Summit.Core", destinationId = singleDestination.Id }).Permission(Permissions.MetaListOwnDestinations));

            menu.Add(T("New Destination"), "1.2",
                     item =>
                     item.Action("Create", "DestinationAdmin", new { area = "Summit.Core" }).Permission(Permissions.ManageDestinations));

        }
    }
}