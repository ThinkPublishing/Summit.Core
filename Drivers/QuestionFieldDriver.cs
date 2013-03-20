// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionFieldDriver.cs" company="EMIS">
//   Copyright (©)2013, EMIS Limited. All rights reserved.
// </copyright>
// <summary>
//   question field driver
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers 
{
    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Localization;

    using Summit.Core.Fields;
    using Summit.Core.Settings;

    public class InputFieldDriver : ContentFieldDriver<QuestionField> {
        public IOrchardServices Services { get; set; }
        private const string TemplateName = "Fields/Question.Edit";

        public InputFieldDriver(IOrchardServices services) {
            Services = services;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        private static string GetPrefix(ContentField field, ContentPart part) {
            return part.PartDefinition.Name + "." + field.Name;
        }

        private static string GetDifferentiator(QuestionField field, ContentPart part) {
            return field.Name;
        }

        protected override DriverResult Display(ContentPart part, QuestionField field, string displayType, dynamic shapeHelper) {
            return ContentShape("Fields_Question", GetDifferentiator(field, part), () => {
                var settings = field.PartFieldDefinition.Settings.GetModel<QuestionFieldSettings>();
                return shapeHelper.Fields_Question().Settings(settings);
            });
        }

        protected override DriverResult Editor(ContentPart part, QuestionField field, dynamic shapeHelper) {
            return ContentShape("Fields_Question_Edit", GetDifferentiator(field, part),
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: field, Prefix: GetPrefix(field, part)));
        }

        protected override DriverResult Editor(ContentPart part, QuestionField field, IUpdateModel updater, dynamic shapeHelper) {
            if (updater.TryUpdateModel(field, GetPrefix(field, part), null, null)) {
                var settings = field.PartFieldDefinition.Settings.GetModel<QuestionFieldSettings>();

                if (settings.Required && string.IsNullOrWhiteSpace(field.Value)) {
                    updater.AddModelError(GetPrefix(field, part), T("The field {0} is mandatory.", T(field.DisplayName)));
                }
            }

            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, QuestionField field, ImportContentContext context) {
            context.ImportAttribute(field.FieldDefinition.Name + "." + field.Name, "Value", v => field.Value = v);
        }

        protected override void Exporting(ContentPart part, QuestionField field, ExportContentContext context) {
            context.Element(field.FieldDefinition.Name + "." + field.Name).SetAttributeValue("Value", field.Value);
        }

        protected override void Describe(DescribeMembersContext context) {
            context
                .Member(null, typeof(string), T("Value"), T("The value of the field."));
        }
    }
}
