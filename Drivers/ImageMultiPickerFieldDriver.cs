// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageMultiPickerFieldDriver.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   image multipicker field driver
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Drivers
{
    using System.Xml.Linq;

    using Orchard;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    using Summit.Core.Fields;
    using Summit.Core.ViewModels;

    public class ImageMultiPickerFieldDriver : FieldDriverBase<ImageMultiPickerField> 
    {
        private IOrchardServices _services { get; set; }

        public ImageMultiPickerFieldDriver(IOrchardServices services)
        {
            _services = services;
        }

        protected override DriverResult Display(ContentPart part, ImageMultiPickerField field, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Fields_ImageMultiPicker", GetDifferentiator(field, part),
                () => shapeHelper.Fields_ImageMultiPicker(Name: field.Name, Field: field));
        }

        protected override DriverResult Editor(ContentPart part, ImageMultiPickerField field, dynamic shapeHelper)
        {
            return ContentShape("Fields_ImageMultiPicker_Edit", GetDifferentiator(field, part),
                () =>
                {
                    var viewModel = new ImageMultiPickerFieldEditorViewModel
                    {
                        Field = field,
                        Data = string.IsNullOrWhiteSpace(field.Data) ? "[]" : field.Data
                    };

                    return shapeHelper.EditorTemplate(TemplateName: "Fields_ImageMultiPicker_Edit", Model: viewModel, Prefix: GetPrefix(field, part));
                });
        }

        protected override DriverResult Editor(ContentPart part, ImageMultiPickerField field, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new ImageMultiPickerFieldEditorViewModel();

            if (updater.TryUpdateModel(viewModel, GetPrefix(field, part), null, null))
            {
                field.Data = viewModel.Data;
            }
            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, ImageMultiPickerField field, ImportContentContext context)
        {
            var imageElement = context.Data.Element(field.FieldDefinition.Name + "." + field.Name);
            if (imageElement == null)
            {
                return;
            }

            var dataElement = imageElement.Element("Data");
            if (dataElement != null)
            {
                field.Data = dataElement.Value;
            }
        }

        protected override void Exporting(ContentPart part, ImageMultiPickerField field, ExportContentContext context)
        {
            var imageElement = context.Element(field.FieldDefinition.Name + "." + field.Name);
            imageElement.Add(new XElement("Data", new XCData(field.Data)));
        }

    }
}