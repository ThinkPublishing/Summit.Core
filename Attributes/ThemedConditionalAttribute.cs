// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemedConditionalAttribute.cs" company="EMIS">
//   Copyright (©)2012, EMIS Limited. All rights reserved.
// </copyright>
// <summary>
//   themed conditional
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Orchard.Themes;

    /// <summary>The themed conditional attribute.</summary>
    public class ThemedConditionalAttribute : Attribute
    {
        public bool ExcludeAjaxRequests { get; set; }
    }
}
