using Summit.Core.Models;
using System;
using Summit.Core.ViewModels;
using System.Linq;

namespace Summit.Core.Helpers {
    public static class TermExtensions {
        public static int GetLevels(this TermPart term) {
            return String.IsNullOrEmpty(term.Path) ? 0 : term.Path.Count( c => c == '/') - 1;
        }

        public static int GetLevels(this TermEntry term) {
            return String.IsNullOrEmpty(term.Path) ? 0 : term.Path.Count( c => c == '/') - 1;
        }

        public static TermEntry CreateTermEntry(this TermPart term) {
            return new TermEntry {
                Id = term.Id,
                Name = term.Name,
                Selectable = term.Selectable,
                Count = term.Count,
                Path = term.Path,
                Weight= term.Weight,
                IsChecked = false,
                ContentItem = term.ContentItem
            };
        }
    }
}