// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntExtension.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Extensions
{
    using System.Linq;

    public static class IntExtension
    {
        public static bool IsLessThen(this int value, params int[] otherNumbers)
        {
            return otherNumbers.All(number => number <= value);
        }
    }
}