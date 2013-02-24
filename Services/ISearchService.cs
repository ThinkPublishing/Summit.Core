using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.ContentManagement;

namespace Summit.Core.Services
{
    public interface ISearchService : IDependency {
        IEnumerable<IContent> Search(string snippet);
    }
}