// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Extensions
{
    using System.Text;

    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object[] args)
        {
            builder.AppendFormat(format, args);
            builder.AppendLine();
            return builder;
        }
    }
}
