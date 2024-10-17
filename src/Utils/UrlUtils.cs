// src/Utils/UrlUtils.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lime.Utils
{
    public static class UrlUtils
    {
        // Encode a string using RFC3986 encoding rules
        public static string Encode(string str)
        {
            return Uri.EscapeDataString(str);
        }

        // Decode a string from URL-encoded format
        public static string Decode(string str)
        {
            return Uri.UnescapeDataString(str.Replace("+", " "));
        }

        // Merges two dictionaries, handling nested dictionaries recursively
        public static IDictionary<string, object?> Merge(IDictionary<string, object?> target, IDictionary<string, object?> source)
        {
            foreach (var keyValuePair in source)
            {
                if (target.ContainsKey(keyValuePair.Key))
                {
                    // If both target and source values are dictionaries, merge recursively
                    if (target[keyValuePair.Key] is IDictionary<string, object?> targetDict &&
                        keyValuePair.Value is IDictionary<string, object?> sourceDict)
                    {
                        target[keyValuePair.Key] = Merge(targetDict, sourceDict);
                    }
                    else
                    {
                        // Otherwise, override with the source value
                        target[keyValuePair.Key] = keyValuePair.Value;
                    }
                }
                else
                {
                    target[keyValuePair.Key] = keyValuePair.Value;
                }
            }
            return target;
        }

        // Compact a dictionary by removing null values or empty nested dictionaries
        public static IDictionary<string, object?> Compact(IDictionary<string, object?> source, Func<object?, bool>? shouldRemove = null)
        {
            var compacted = new Dictionary<string, object?>();

            // Default criteria for removal: null values
            shouldRemove ??= value => value == null;

            foreach (var keyValuePair in source)
            {
                if (keyValuePair.Value is IDictionary<string, object?> nestedDict)
                {
                    var nestedCompacted = Compact(nestedDict, shouldRemove);
                    if (nestedCompacted.Count > 0)
                    {
                        compacted[keyValuePair.Key] = nestedCompacted;
                    }
                }
                else if (keyValuePair.Value is IEnumerable<object?> collection && !(keyValuePair.Value is string))
                {
                    // Handle collections
                    var compactedCollection = collection.Where(item => !shouldRemove(item)).ToList();
                    if (compactedCollection.Any())
                    {
                        compacted[keyValuePair.Key] = compactedCollection;
                    }
                }
                else if (!shouldRemove(keyValuePair.Value))
                {
                    compacted[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            return compacted;
        }

        // Combine two collections into one, handling duplicates
        public static IEnumerable<T> Combine<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            return a.Concat(b);
        }

        // Map a function to each element of a collection if it is an IEnumerable
        public static IEnumerable<T> MaybeMap<T>(IEnumerable<T> val, Func<T, T> fn)
        {
            return val.Select(fn);
        }
    }
}
