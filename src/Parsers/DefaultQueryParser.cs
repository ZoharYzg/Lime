// src/Parsers/DefaultQueryParser.cs (Updated)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lime.Utils;

namespace Lime.Parsers
{
    public class DefaultQueryParser : IQueryParser
    {
        public async Task<IDictionary<string, object?>> ParseAsync(string queryString, ParserOptions? options = null)
        {
            var config = options ?? ParserOptions.Default;
            var result = new Dictionary<string, object?>();

            if (string.IsNullOrEmpty(queryString))
            {
                return await Task.FromResult(result);
            }

            // Remove leading '?' if specified in options
            var cleanQueryString = config.IgnoreQueryPrefix && queryString.StartsWith("?")
                ? queryString.Substring(1)
                : queryString;

            // Split the query string by the delimiter
            var pairs = cleanQueryString.Split(new[] { config.Delimiter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                var keyValue = pair.Split(new[] { '=' }, 2);
                var key = config.Decoder(keyValue[0]);
                var value = keyValue.Length > 1 ? config.Decoder(keyValue[1]) : null;

                // Handle dots in keys for nested structures
                if (config.AllowDots && key.Contains("."))
                {
                    AddNestedValue(result, key.Split('.'), value, config);
                }
                else
                {
                    AddValue(result, key, value, config);
                }
            }

            // Compact the result to remove unnecessary entries based on custom criteria
            var compactedResult = UrlUtils.Compact(result, value => value == null || (value is string str && string.IsNullOrWhiteSpace(str)));
            
            // Convert to a concrete Dictionary<string, object?> before returning
            return await Task.FromResult(new Dictionary<string, object?>(compactedResult));
        }

        private void AddValue(IDictionary<string, object?> result, string key, object? value, ParserOptions config)
        {
            if (result.ContainsKey(key))
            {
                switch (config.DuplicatesHandling)
                {
                    case DuplicateHandling.Combine:
                        if (result[key] is List<object?> existingList)
                        {
                            existingList.Add(value);
                        }
                        else
                        {
                            result[key] = new List<object?> { result[key], value };
                        }
                        break;
                    case DuplicateHandling.First:
                        // Keep the first value, do nothing
                        break;
                    case DuplicateHandling.Last:
                        result[key] = value;
                        break;
                }
            }
            else
            {
                result[key] = value;
            }
        }

        private void AddNestedValue(IDictionary<string, object?> result, string[] keys, object? value, ParserOptions config)
        {
            var current = result;

            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                if (i == keys.Length - 1)
                {
                    AddValue(current, key, value, config);
                }
                else
                {
                    if (!current.ContainsKey(key) || !(current[key] is IDictionary<string, object?> nestedDict))
                    {
                        nestedDict = new Dictionary<string, object?>();
                        current[key] = nestedDict;
                    }
                    current = (IDictionary<string, object?>)current[key]!;
                }
            }
        }
    }
}
