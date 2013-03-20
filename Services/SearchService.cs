// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchService.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Services {
    using System.Collections.Generic;
    using System.Linq;

    using Orchard.ContentManagement;
    using Orchard.Core.Title.Models;

    public class SearchService : ISearchService {
        private readonly IContentManager contentManager;

        public SearchService(IContentManager contentManager) {
            this.contentManager = contentManager;
        }

        public int SearchCount(string search) {
            return this.SearchQuery(search, null, null).Count();
        }

        public IEnumerable<IContent> Search(string search, int? skip, int? count) {
            return this.SearchQuery(search, skip, count).ToList();
        }

        public IEnumerable<IContent> SearchBySnippet(string snippet) {
            var results = new List<IContent>();
            results.AddRange(this.contentManager.Query("Hotel").Join<TitlePartRecord>().Where(x => x.Title.StartsWith(snippet)).List().ToList());
            results.AddRange(this.contentManager.Query("Destination").Join<TitlePartRecord>().Where(x => x.Title.StartsWith(snippet)).List().ToList());
            return results;
        }

        private IQueryable<IContent> SearchQuery(string search, int? skip, int? count) {
            var query = this.contentManager.Query(new[]  { "Hotel", "Destination"}).Where<TitlePartRecord>(x => x.Title.Contains(search));

            if (skip.HasValue && count.HasValue) {
                return query.Slice(skip.Value, count.Value).AsQueryable();
            }

            return query.List().AsQueryable();
        }
    }
}