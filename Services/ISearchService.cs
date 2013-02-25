using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.ContentManagement;

namespace Summit.Core.Services
{
    public interface ISearchService : IDependency {
        int SearchCount(string search);
        IEnumerable<IContent> Search(string search, int? skip, int? count);
        IEnumerable<IContent> SearchBySnippet(string snippet);
    }
}