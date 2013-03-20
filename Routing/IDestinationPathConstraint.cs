// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDestinationPathConstraint.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   constraint for destination paths
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Routing
{
    using System.Collections.Generic;
    using System.Web.Routing;

    using Orchard;

    public interface IDestinationPathConstraint : IRouteConstraint, ISingletonDependency
    {
        void SetPaths(IEnumerable<string> paths);
        string FindPath(string path);
        void AddPath(string path);
        void RemovePath(string path);
    }
}