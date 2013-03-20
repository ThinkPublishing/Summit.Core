// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaxonomySlugConstraint.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Routing 
{
    using System.Collections.Generic;
    using System.Web.Routing;

    using Orchard;

    public interface ITaxonomySlugConstraint : IRouteConstraint, ISingletonDependency 
    {
        void SetSlugs(IEnumerable<string> slugs);
        string FindSlug(string slug);
        void AddSlug(string slug);
        void RemoveSlug(string slug);
    }
}