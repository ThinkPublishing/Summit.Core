using System;
using Orchard.ContentManagement;
using Orchard.ContentManagement.FieldStorage;

namespace Summit.Core.Fields {
    public class QuestionField : ContentField {
        
        public string Value {
            get { return Storage.Get<string>(); }
            set { Storage.Set(value ?? String.Empty); }
        }
    }
}
