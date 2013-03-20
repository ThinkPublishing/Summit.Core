// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionField.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Fields
{
    using System;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.FieldStorage;

    public class QuestionField : ContentField
    {

        public string Value
        {
            get
            {
                return Storage.Get<string>();
            }
            set
            {
                Storage.Set(value ?? String.Empty);
            }
        }
    }
}
