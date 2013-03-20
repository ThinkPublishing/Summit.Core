// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentExtensions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Extensions
{
    using System.Linq;

    using Orchard.ContentManagement;

    public static class ContentExtensions
    {
        /// <summary>The as.</summary>
        /// <param name="content">The content.</param>
        /// <param name="partDefinitionName">The part definition name.</param>
        /// <returns>The Orchard.ContentManagement.IContent.</returns>
        public static ContentPart As(this IContent content, string partDefinitionName)
        {
            return content.ContentItem.Parts.FirstOrDefault(p => p.PartDefinition.Name == partDefinitionName);
        }

        public static bool Has(this IContent content, string partDefinitionName)
        {
            return content.ContentItem.Parts.Any(p => p.PartDefinition.Name == partDefinitionName);
        }

        /// <summary>The get property.</summary>
        /// <param name="content">The content.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The System.Object.</returns>
        public static object GetProperty(this IContent content, string propertyName)
        {
            return content.GetType().GetProperty(propertyName).GetValue(content, null);
        }

        /// <summary>The get field value.</summary>
        /// <param name="content">The content.</param>
        /// <param name="fieldName">The field name.</param>
        /// <param name="fieldProperty">The field property.</param>
        /// <returns>The System.String.</returns>
        public static string GetFieldValue(this IContent content, string fieldName, string fieldProperty)
        {
            return content.GetFieldValue(null, fieldName, fieldProperty);
        }

        /// <summary>The get field value.</summary>
        /// <param name="content">The content.</param>
        /// <param name="partDefinitionName">The part definition name.</param>
        /// <param name="fieldName">The field name.</param>
        /// <param name="fieldProperty">The field property.</param>
        /// <returns>The System.String.</returns>
        public static string GetFieldValue(
            this IContent content, string partDefinitionName, string fieldName, string fieldProperty)
        {
            var val = string.Empty;
            var part = string.IsNullOrEmpty(partDefinitionName) ? content : content.As(partDefinitionName);

            if (part != null)
            {
                var cast = (ContentPart)part;
                var field = cast.Fields.FirstOrDefault(x => x.Name == fieldName);

                if (field != null)
                {
                    val = field.Storage.Get<string>(fieldProperty);
                }
            }

            return val;
        }

        public static void SetFieldValue(this IContent content, string fieldName, string fieldProperty, string value)
        {
            content.SetFieldValue(null, fieldName, fieldProperty, value);
        }

        public static void SetFieldValue(
            this IContent content, string partDefinitionName, string fieldName, string fieldProperty, string value)
        {
            var val = string.Empty;
            var part = string.IsNullOrEmpty(partDefinitionName) ? content : content.As(partDefinitionName);

            if (part != null)
            {
                var cast = (ContentPart)part;
                var field = cast.Fields.FirstOrDefault(x => x.Name == fieldName);

                if (field != null)
                {
                    field.Storage.Set<string>(fieldProperty, value);
                }
            }
        }
    }
}