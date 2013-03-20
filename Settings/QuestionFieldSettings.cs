// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionFieldSettings.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Settings {


    public class QuestionFieldSettings {
        public string Title { get; set; }
        public string Hint { get; set; }
        public bool Required { get; set; }
        public bool AutoFocus { get; set; }
        public bool AutoComplete { get; set; }
        public string Placeholder { get; set; }
        public string Pattern { get; set; }
        public string EditorCssClass { get; set; }
        public int MaxLength { get; set; }

        public string Question1 { get; set; }
        public string Question2 { get; set; }

    }
}
