// src/Extensions/QueryStringExtensions.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lime.Core;
using Lime.Parsers;
using Lime.Stringifiers;

namespace Lime.Extensions
{
    public static class QueryStringExtensions
    {
        // Extension method to convert a dictionary to a query string
        public static async Task<string> ToQueryStringAsync(this IDictionary<string, object?> data, StringifierOptions? options = null)
        {
            var manager = new QueryStringManager(opt =>
            {
                opt.UseStringifierOptions(options ?? StringifierOptions.Default);
            });
            return await manager.StringifyAsync(data);
        }

        // Extension method to parse a query string into a dictionary
        public static async Task<IDictionary<string, object?>> ParseQueryStringAsync(this string queryString, ParserOptions? options = null)
        {
            var manager = new QueryStringManager(opt =>
            {
                opt.UseParserOptions(options ?? ParserOptions.Default);
            });
            return await manager.ParseAsync(queryString);
        }
    }
}