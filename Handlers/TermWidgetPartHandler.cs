// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TermWidgetPartHandler.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Handlers
{
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    using Summit.Core.Models;

    public class TermWidgetPartHandler : ContentHandler
    {
        public TermWidgetPartHandler(IRepository<TermWidgetPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}