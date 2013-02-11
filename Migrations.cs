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

            ContentDefinitionManager.AlterTypeDefinition("Hotel",
                 cfg => cfg
                     .WithPart("CommonPart", p => p
                        .WithSetting("DateEditorSettings.ShowDateEditor", "true"))
                     .WithPart("PublishLaterPart")
                     .WithPart("TitlePart")
                     .WithPart("AutoroutePart", builder => builder
                         .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                         .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                         .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: 'hotel/{Content.Slug}', Description: 'hotel/my-hotel'}]")
                         .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                     .WithPart("HotelPart")
                     .Draftable()
                     .Creatable()
                 );

            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("DestinationPart",
                cfg => cfg
                        .WithField("Name", field => field.OfType("TextField").WithDisplayName("Destination Name"))
                        .WithField("Image", field => field.OfType("ImageField").WithDisplayName("Destination Image"))
                        .WithField("Country", field => field.OfType("TextField").WithDisplayName("Destination Country"))
                        .WithField("Region", field => field.OfType("Region").WithDisplayName("Destination Region")));

            ContentDefinitionManager.AlterTypeDefinition("Destination",
                cfg => cfg
                    .WithPart("CommonPart", p => p
                       .WithSetting("DateEditorSettings.ShowDateEditor", "true"))
                    .WithPart("TitlePart")
                    .WithPart("AutoroutePart", builder => builder
                        .WithSetting("AutorouteSettings.AllowCustomPattern", "false")
                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Slug}', Description: 'my-destination'}]")
                        .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                    .WithPart("DestinationPart")
                    .Creatable()
                );

            return 2;
        }
    }
}