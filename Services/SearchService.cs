using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Records;
using Orchard.Core.Title.Models;

namespace Summit.Core.Services
{
    public class SearchService : ISearchService {
        private readonly IContentManager contentManager;

        public SearchService(IContentManager contentManager) {
            this.contentManager = contentManager;
        }

        public IEnumerable<IContent> Search(string snippet) {
            var results = new List<IContent>();
            results.AddRange(this.contentManager.Query("Hotel").Join<TitlePartRecord>().Where(x => x.Title.StartsWith(snippet)).List().ToList());
            results.AddRange(this.contentManager.Query("Destination").Join<TitlePartRecord>().Where(x => x.Title.StartsWith(snippet)).List().ToList());
            return results;
        }
    }
}