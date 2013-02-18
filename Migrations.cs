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
                        .WithField("Active", field => field.OfType("BooleanField").WithDisplayName("Active"))
                        .WithField("Conceirge", field => field.OfType("ContentPickerField")
                                .WithSetting("ContentPickerField.Required", "true")
                                .WithSetting("ContentPickerField.Multiple", "false"))
                        );

            ContentDefinitionManager.AlterPartDefinition("LocationPart",
                cfg => cfg
                        .WithField("Address", field => field.OfType("TextField").WithDisplayName("Address"))
                        .WithField("City", field => field.OfType("TextField").WithDisplayName("City"))
                        .WithField("ProvinceState", field => field.OfType("TextField").WithDisplayName("Province or State"))
                        .WithField("Country", field => field.OfType("TextField").WithDisplayName("Country"))
                        .WithField("Postcode", field => field.OfType("TextField").WithDisplayName("Postcode"))
                        );

            ContentDefinitionManager.AlterPartDefinition("DestinationPart",
                cfg => cfg
                        .WithField("Region", field => field.OfType("TextField").WithDisplayName("Region"))
                        .WithField("Country", field => field.OfType("TextField").WithDisplayName("Country"))
                        .WithField("Logo", field => field.OfType("ImageField").WithDisplayName("Image"))
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
                         .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Container.Path}/{Content.Slug}', Description: 'destination/my-hotel'}]")
                         .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                     .WithPart("HotelPart")
                     .WithPart("LocationPart")
                     .Draftable()
                 );

            ContentDefinitionManager.AlterTypeDefinition("Destination", 
                cfg => cfg
                    .WithPart("CommonPart", p => p
                        .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                    .WithPart("TitlePart")
                    .WithPart("AutoroutePart", builder => builder
                        .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Slug}', Description: 'my-destination'}]")
                        .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                    .WithPart("DestinationPart")
                );

            ContentDefinitionManager.AlterPartDefinition("ConceirgePart",
                    cfg => cfg
                            .WithField("Position", field => field.OfType("TextField").WithDisplayName("Position"))
                            .WithField("ServiceLength", field => field.OfType("TextField").WithDisplayName("Length of service"))
                            .WithField("StrangestRequest", field => field.OfType("TextField").WithDisplayName("Strangest request"))
                            .WithField("FavouriteMoment", field => field.OfType("TextField").WithDisplayName("Favourite moment"))
                            .WithField("FavouriteRoom", field => field.OfType("TextField").WithDisplayName("Favourite room in the hotel"))
                            .WithField("Image", field => field.OfType("ImageField"))
                            .WithField("Active", field => field.OfType("BooleanField").WithDisplayName("Active"))
                            .WithField("Question1", field => field.OfType("TextField"))
                            .WithField("Question2", field => field.OfType("TextField"))
                            .WithField("Question3", field => field.OfType("TextField"))
                            .WithField("Question4", field => field.OfType("TextField"))
                            .WithField("Question5", field => field.OfType("TextField"))
                            .WithField("Question6", field => field.OfType("TextField"))
                            .WithField("Question7", field => field.OfType("TextField"))
                            .WithField("Question8", field => field.OfType("TextField"))
                            .WithField("Question9", field => field.OfType("TextField"))
                            .WithField("Question10", field => field.OfType("TextField"))
                            .WithField("Question11", field => field.OfType("TextField"))
                            .WithField("Question12", field => field.OfType("TextField"))
                            .WithField("Question13", field => field.OfType("TextField"))
                            .WithField("Question14", field => field.OfType("TextField"))
                            .WithField("Question15", field => field.OfType("TextField"))
                );

            ContentDefinitionManager.AlterTypeDefinition("Conceirge",
              cfg => cfg
                  .WithPart("CommonPart", p => p
                      .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                  .WithPart("TitlePart")
                  .WithPart("ConceirgePart")
              );

            return 1;
        }
    }
}