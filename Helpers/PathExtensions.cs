// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathExtensions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Helpers
{
    using System;
    using System.Linq;

    using Summit.Core.Models;
    using Summit.Core.ViewModels;

    public static class TermExtensions
    {
        public static int GetLevels(this TermPart term)
        {
            return String.IsNullOrEmpty(term.Path) ? 0 : term.Path.Count(c => c == '/') - 1;
        }

        public static int GetLevels(this TermEntry term)
        {
            return String.IsNullOrEmpty(term.Path) ? 0 : term.Path.Count(c => c == '/') - 1;
        }

        public static TermEntry CreateTermEntry(this TermPart term)
        {
            return new TermEntry
                {
                    Id = term.Id,
                    Name = term.Name,
                    Selectable = term.Selectable,
                    Count = term.Count,
                    Path = term.Path,
                    Weight = term.Weight,
                    IsChecked = false,
                    ContentItem = term.ContentItem
                };
        }
    }
}