// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JSONExtensions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   FileDescription
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Extensions
{
    using Newtonsoft.Json;

    internal static class JsonExtensions
    {
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string json)
            where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
