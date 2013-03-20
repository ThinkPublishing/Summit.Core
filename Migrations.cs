// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migrations.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core {
    using System;

    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Contents.Extensions;
    using Orchard.Data.Migration;

    using Summit.Core.Models;

    public class Migration : DataMigrationImpl {

        public int Create() {
            const string allowedImageExtensions = "jpg jpeg png gif";

            SchemaBuilder.CreateTable(typeof(ImagePowerToolsSettingsRecord).Name,
                table => table
                    .Column<int>("Id", c => c.PrimaryKey().Identity())
                    .Column<int>("MaxCacheSizeMB")
                    .Column<int>("MaxCacheAgeDays")
                    .Column<bool>("EnableFrontendResizeAction")
                    .Column<int>("MaxImageWidth")
                    .Column<int>("MaxImageHeight")
                    .Column<DateTime>("DeleteOldLastJobRun")
                );

            ContentDefinitionManager.AlterTypeDefinition("Destination",
             cfg => cfg
                 .WithPart("CommonPart", p => p
                     .WithSetting("DateEditorSettings.ShowDateEditor", "false")
                     .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                 .WithPart("TitlePart")
                 .WithPart("AutoroutePart", builder => builder
                     .WithSetting("AutorouteSettings.AllowCustomPattern", "false")
                     .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "true")
                     .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Slug}', Description: 'my-destination'}]")
                     .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                 .WithPart("DestinationPart")
             );

            ContentDefinitionManager.AlterPartDefinition("ConciergePart",
                    cfg => cfg
                            .WithField("Position", field => field.OfType("TextField").WithDisplayName("Position"))
                            .WithField("ServiceLength", field => field.OfType("TextField").WithDisplayName("Length of service"))
                            .WithField("StrangestRequest", field => field.OfType("TextField").WithDisplayName("Strangest request"))
                            .WithField("FavouriteMoment", field => field.OfType("TextField").WithDisplayName("Favourite moment"))
                            .WithField("FavouriteRoom", field => field.OfType("TextField").WithDisplayName("Favourite room in the hotel"))
                            .WithField("Image", field => field.OfType("MediaPickerField").WithSetting("MediaPickerFieldSettings.Required", "true").WithSetting("MediaPickerFieldSettings.AllowedExtensions", allowedImageExtensions))
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

            ContentDefinitionManager.AlterTypeDefinition("Concierge",
              cfg => cfg
                  .WithPart("IdentityPart")
                  .WithPart("CommonPart", p => p
                      .WithSetting("DateEditorSettings.ShowDateEditor", "false")
                      .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                  .WithPart("TitlePart")
                  .WithPart("ConciergePart")
              );
           
            ContentDefinitionManager.AlterPartDefinition("HotelPart",
                cfg => cfg
                        .WithField("Description", field => field.OfType("TextField").WithDisplayName("Description").WithSetting("TextFieldSettings.Flavor", "Html"))
                        .WithField("Image", field => field.OfType("MediaPickerField").WithSetting("MediaPickerFieldSettings.Required", "true").WithSetting("MediaPickerFieldSettings.AllowedExtensions", allowedImageExtensions))
                        .WithField("Phone", field => field.OfType("TextField").WithDisplayName("Phone Number"))
                        .WithField("ReservationUrl", field => field.OfType("TextField").WithDisplayName("Reservation Url"))
                        .WithField("Status", field => field.OfType("EnumerationField").WithDisplayName("Status").WithSetting("EnumerationFieldSettings.Options", "Active\r\nDecommisioned"))
                        .WithField("Concierge", field => field.OfType("ContentPickerField")
                                .WithSetting("ContentPickerFieldSettings.Required", "true")
                                .WithSetting("ContentPickerFieldSettings.Multiple", "false"))
                        );

            ContentDefinitionManager.AlterPartDefinition("AddressPart",
                cfg => cfg
                        .WithField("Address", field => field.OfType("TextField").WithDisplayName("Address").WithSetting("TextField.Required", "true"))
                        .WithField("City", field => field.OfType("TextField").WithDisplayName("City"))
                        .WithField("ProvinceState", field => field.OfType("TextField").WithDisplayName("Province or State"))
                        .WithField("Country", field => field.OfType("TextField").WithDisplayName("Country").WithSetting("TextField.Required", "true"))
                        .WithField("Postcode", field => field.OfType("TextField").WithDisplayName("Postcode/Zipcode").WithSetting("TextField.Required", "true"))
                        );

            ContentDefinitionManager.AlterPartDefinition("DestinationPart",
                cfg => cfg
                        .WithField("Quote", field => field.OfType("TextField").WithDisplayName("Quote").WithSetting("TextFieldSettings.Flavor", "Html"))
                        .WithField("Region", field => field.OfType("TaxonomyField").WithDisplayName("Region").WithSetting("TaxonomyFieldSettings.Taxonomy", "Region").WithSetting("TaxonomyFieldSettings.LeavesOnly", "true").WithSetting("TaxonomyFieldSettings.SingleChoice", "true"))
                        .WithField("Gallery", field => field.OfType("ImageMultiPickerField"))
                        .WithField("Featured", field => field.OfType("BooleanField").WithDisplayName("Featured"))
                        );

            ContentDefinitionManager.AlterTypeDefinition("Hotel",
                 cfg => cfg
                     .WithPart("CommonPart", p => p
                        .WithSetting("DateEditorSettings.ShowDateEditor", "false")
                        .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                     .WithPart("TitlePart")
                     .WithPart("AutoroutePart", builder => builder
                         .WithSetting("AutorouteSettings.AllowCustomPattern", "false")
                         .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "true")
                         .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Container.Path}/{Content.Slug}', Description: 'destination/my-hotel'}]")
                         .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                     .WithPart("HotelPart")
                     .WithPart("AddressPart")
                     .Draftable()
                 );


            ContentDefinitionManager.AlterTypeDefinition("SearchWidget",
                cfg => cfg
                    .WithPart("SearchWidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            // taxonomies
            SchemaBuilder.CreateTable("TaxonomyPartRecord",
              table => table
                  .ContentPartRecord()
                  .Column<string>("TermTypeName", column => column.WithLength(255))
              );

            SchemaBuilder.CreateTable("TermPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Path", column => column.WithLength(255))
                    .Column<int>("TaxonomyId")
                    .Column<int>("Count")
                    .Column<int>("Weight")
                    .Column<bool>("Selectable")
                );

            SchemaBuilder.CreateTable("TermContentItem",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Field", column => column.WithLength(50))
                    .Column<int>("TermRecord_id")
                    .Column<int>("TermsPartRecord_id")
                );

            SchemaBuilder.CreateTable("TaxonomyMenuPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("TaxonomyPartRecord_id")
                    .Column<int>("TermPartRecord_id")
                    .Column<bool>("DisplayTopMenuItem")
                    .Column<int>("LevelsToDisplay")
                    .Column<bool>("DisplayContentCount")
                    .Column<bool>("HideEmptyTerms")
                );

            ContentDefinitionManager.AlterTypeDefinition("Taxonomy",
                 cfg => cfg
                     .WithPart("TaxonomyPart")
                     .WithPart("CommonPart")
                     .WithPart("TitlePart")
                     .WithPart("AutoroutePart", builder => builder
                        .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Slug}', Description: 'my-taxonomy'}]")
                        .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                );

            ContentDefinitionManager.AlterTypeDefinition("TaxonomyMenu",
                cfg => cfg
                    .WithPart("TaxonomyMenuPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            SchemaBuilder.CreateTable("TaxonomyMenuItemPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<bool>("RenderMenuItem")
                    .Column<string>("Position", c => c.WithLength(30))
                    .Column<string>("Name", c => c.WithLength(255))
                );

            SchemaBuilder.CreateTable("TermsPartRecord",
                table => table
                    .ContentPartRecord()
                );

            SchemaBuilder.CreateTable("TermWidgetPartRecord",
                            table => table
                                .ContentPartRecord()
                                .Column<int>("TaxonomyPartRecord_id")
                                .Column<int>("TermPartRecord_id")
                                .Column<int>("Count")
                                .Column<string>("OrderBy")
                                .Column<string>("FieldName")
                                .Column<string>("ContentType", c => c.Nullable())
                            );

            ContentDefinitionManager.AlterTypeDefinition("TermWidget",
                cfg => cfg
                    .WithPart("TermWidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            return 1;
        }
    }
}