// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionFieldEditorEvents.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Settings {
    using System.Collections.Generic;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.MetaData;
    using Orchard.ContentManagement.MetaData.Builders;
    using Orchard.ContentManagement.MetaData.Models;
    using Orchard.ContentManagement.ViewModels;

    public class QuestionFieldListModeEvents : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "QuestionField") {
                var model = definition.Settings.GetModel<QuestionFieldSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "QuestionField") {
                yield break;
            }

            var model = new QuestionFieldSettings();
            if (updateModel.TryUpdateModel(model, "QuestionFieldSettings", null, null)) {
                builder.WithSetting("QuestionFieldSettings.Title", model.Title);
                builder.WithSetting("QuestionFieldSettings.Hint", model.Hint);
                builder.WithSetting("QuestionFieldSettings.Required", model.Required.ToString());
                builder.WithSetting("QuestionFieldSettings.AutoFocus", model.AutoFocus.ToString());
                builder.WithSetting("QuestionFieldSettings.AutoComplete", model.AutoComplete.ToString());
                builder.WithSetting("QuestionFieldSettings.Placeholder", model.Placeholder);
                builder.WithSetting("QuestionFieldSettings.Pattern", model.Pattern);
                builder.WithSetting("QuestionFieldSettings.EditorCssClass", model.EditorCssClass);
                builder.WithSetting("QuestionFieldSettings.MaxLength", model.MaxLength.ToString());
                builder.WithSetting("QuestionFieldSettings.Question1", model.Question1);
                builder.WithSetting("QuestionFieldSettings.Question1", model.Question2);
            }

            yield return DefinitionTemplate(model);
        }
    }
}