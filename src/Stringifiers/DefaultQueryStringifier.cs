// src/Stringifiers/DefaultQueryStringifier.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lime.Utils;

namespace Lime.Stringifiers
{
    public class DefaultQueryStringifier : IQueryStringifier
    {
        public async Task<string> StringifyAsync(IDictionary<string, object?> data, StringifierOptions? options = null)
        {
            var config = options ?? StringifierOptions.Default;
            var keys = new List<string>();

            foreach (var (key, value) in data)
            {
                if (value == null && config.SkipNulls)
                    continue;

                var encodedKey = config.Encode ? config.Encoder(key) : key;
                StringifyValue(value, encodedKey, config, keys);
            }

            var joined = string.Join(config.Delimiter, keys);
            return await Task.FromResult(config.AddQueryPrefix ? "?" + joined : joined);
        }

        private void StringifyValue(object? value, string prefix, StringifierOptions config, List<string> keys)
        {
            if (value == null)
            {
                if (config.StrictNullHandling)
                {
                    keys.Add(prefix);
                }
                else
                {
                    keys.Add($"{prefix}=");
                }
                return;
            }

            switch (value)
            {
                case string strValue:
                    keys.Add($"{prefix}={EncodeValue(strValue, config)}");
                    break;

                case DateTime dateTimeValue:
                    var dateValue = config.SerializeDate(dateTimeValue);
                    keys.Add($"{prefix}={EncodeValue(dateValue, config)}");
                    break;

                case IEnumerable enumerable when !(value is string):
                    var enumerablePrefix = GetArrayPrefix(prefix, config.ArrayFormat);
                    foreach (var item in enumerable)
                    {
                        StringifyValue(item, enumerablePrefix, config, keys);
                    }
                    break;

                case IDictionary<string, object?> dictionary:
                    foreach (var (key, dictValue) in dictionary)
                    {
                        var nestedPrefix = $"{prefix}[{key}]";
                        StringifyValue(dictValue, nestedPrefix, config, keys);
                    }
                    break;

                default:
                    keys.Add($"{prefix}={EncodeValue(value.ToString() ?? string.Empty, config)}");
                    break;
            }
        }

        private string EncodeValue(string value, StringifierOptions config)
        {
            return config.Encode ? config.Encoder(value) : value;
        }

        private string GetArrayPrefix(string prefix, ArrayFormat arrayFormat)
        {
            return arrayFormat switch
            {
                ArrayFormat.Brackets => $"{prefix}[]",
                ArrayFormat.Indices => $"{prefix}[0]", // Can be adjusted to support index iteration if needed
                ArrayFormat.Repeat => prefix,
                ArrayFormat.Comma => prefix, // We'll join comma values at a higher level
                _ => prefix
            };
        }
    }
}
