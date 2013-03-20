// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemedConditionalAttribute.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   Conditional theme attribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Attributes
{
    using System;

    /// <summary>The themed conditional attribute.</summary>
    public class ThemedConditionalAttribute : Attribute
    {
        public bool ExcludeAjaxRequests { get; set; }
    }
}
