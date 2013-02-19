using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Projections.Models;
using Orchard.Projections.Services;

namespace Summit.Core {
    public class UsersDataMigration : DataMigrationImpl {

        private readonly IQueryService queryService;
        public UsersDataMigration(IQueryService queryService) {
            this.queryService = queryService;
        }


        public int Create() {
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
                            .WithField("Question1", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "Three words to describe the location").WithSetting("QuestionFieldSettings.Question2", "Three words to describe the location"))
                            .WithField("Question2", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the first thing anyone new to [location] should do").WithSetting("QuestionFieldSettings.Question2", "What’s the first thing anyone new to [location] should do"))
                            .WithField("Question3", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "Tell us something surprising about [location]").WithSetting("QuestionFieldSettings.Question2", "Tell us something surprising about [location]"))
                            .WithField("Question4", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s your favourite spot in [location]").WithSetting("QuestionFieldSettings.Question2", "What’s your favourite spot in [location]"))
                            .WithField("Question5", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "Where’s the best shopping street").WithSetting("QuestionFieldSettings.Question2", "Where’s the best souvenir to take home"))
                            .WithField("Question6", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s your top cultural highlight").WithSetting("QuestionFieldSettings.Question2", "Where’s the best place for a shot of culture"))
                            .WithField("Question7", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the best place to sit back watch the world go by").WithSetting("QuestionFieldSettings.Question2", "What’s the best place to sit back watch the world go by"))
                            .WithField("Question8", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the hottest lunch spot right now").WithSetting("QuestionFieldSettings.Question2", "Where’s a great place for lunch"))
                            .WithField("Question9", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the quirkiest attraction in [location]").WithSetting("QuestionFieldSettings.Question2", "What’s the quirkiest attraction in [location]"))
                            .WithField("Question10", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What would you recommend for a great family outing").WithSetting("QuestionFieldSettings.Question2", "What would you recommend for a great family outing"))
                            .WithField("Question11", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the best place to relax in [location]").WithSetting("QuestionFieldSettings.Question2", "What’s the best place to relax in [location]"))
                            .WithField("Question12", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "Where would you find the best view of [location]").WithSetting("QuestionFieldSettings.Question2", "Where would you find the best view of [location]"))
                            .WithField("Question13", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the drink everyone is ordering right now").WithSetting("QuestionFieldSettings.Question2", "What’s the drink everyone is ordering right now"))
                            .WithField("Question14", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "Where would you recommend for a romantic dinner").WithSetting("QuestionFieldSettings.Question2", "Where would you recommend for a romantic dinner"))
                            .WithField("Question15", field => field.OfType("QuestionField").WithSetting("QuestionFieldSettings.Question1", "What’s the current hotspot for nightlife?").WithSetting("QuestionFieldSettings.Question2", "What’s the current hotspot for nightlife?"))
                );

            ContentDefinitionManager.AlterTypeDefinition("Conceirge",
              cfg => cfg
                  .WithPart("CommonPart", p => p
                      .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
                  .WithPart("TitlePart")
                  .WithPart("ConceirgePart")
              );

            var query = this.queryService.CreateQuery("List of Active Conceirges");
           

            ContentDefinitionManager.AlterPartDefinition("HotelPart",
                cfg => cfg
                        .WithField("Description", field => field.OfType("TextField").WithDisplayName("Quote").WithSetting("TextFieldSettings.Flavor", "Html"))
                        .WithField("Logo", field => field.OfType("ImageField").WithDisplayName("Logo"))
                        .WithField("Phone", field => field.OfType("TextField").WithDisplayName("Phone Number"))
                        .WithField("ReservationUrl", field => field.OfType("TextField").WithDisplayName("Reservation Url"))
                        .WithField("Active", field => field.OfType("BooleanField").WithDisplayName("Active"))
                        .WithField("Conceirge", field => field.OfType("ContentPickerField")
                                .WithSetting("ContentPickerFieldSettings.QueryId", query.Id.ToString())
                                .WithSetting("ContentPickerFieldSettings.Required", "true")
                                .WithSetting("ContentPickerFieldSettings.Multiple", "false"))
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

         
            

            return 1;
        }
    }
}