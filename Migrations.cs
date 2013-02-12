using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Summit.Core {
    public class UsersDataMigration : DataMigrationImpl {

        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("HotelPart",
                cfg => cfg
                        .WithField("Description", field => field.OfType("TextField").WithDisplayName("Description").WithSetting("Flavor", "Html"))
                        .WithField("Logo", field => field.OfType("ImageField").WithDisplayName("Logo"))
                        .WithField("Phone", field => field.OfType("TextField").WithDisplayName("Phone Number"))
                        .WithField("ReservationUrl", field => field.OfType("TextField").WithDisplayName("Reservation Url"))
                        .WithField("Active", field => field.OfType("BooleanField").WithDisplayName("Active")));

            ContentDefinitionManager.AlterPartDefinition("LocationPart",
                cfg => cfg
                        .WithField("Address", field => field.OfType("TextField").WithDisplayName("Address"))
                        .WithField("City", field => field.OfType("TextField").WithDisplayName("City"))
                        .WithField("ProvinceState", field => field.OfType("TextField").WithDisplayName("Province or State"))
                        .WithField("Country", field => field.OfType("TextField").WithDisplayName("Country"))
                        .WithField("Postcode", field => field.OfType("TextField").WithDisplayName("Postcode"))
                        );

            ContentDefinitionManager.AlterTypeDefinition("Hotel",
                 cfg => cfg
                     .WithPart("CommonPart", p => p
                        .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                     .WithPart("PublishLaterPart")
                     .WithPart("TitlePart")
                     .WithPart("AutoroutePart", builder => builder
                         .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                         .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                         .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: 'hotel/{Content.Slug}', Description: 'hotel/my-hotel'}]")
                         .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                     .WithPart("HotelPart")
                     .WithPart("LocationPart")
                     .Draftable()
                     .Creatable()
                 );

            return 1;
        }

        public int UpdateFrom1()
        {
            

            ContentDefinitionManager.AlterTypeDefinition("Hotel", cfg => cfg.WithPart("LocationPart"));

            return 2;
        }
    }
}