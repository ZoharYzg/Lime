// src/Core/QueryStringManager.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lime.Parsers;
using Lime.Stringifiers;

namespace Lime.Core
{
    public class QueryStringManager
    {
        private readonly LimeOptions _options;

        // Constructor with optional configuration action
        public QueryStringManager(Action<LimeOptions>? configureOptions = null)
        {
            _options = new LimeOptions();
            configureOptions?.Invoke(_options); // Apply custom configuration if provided
        }

        // Method to parse a query string into a dictionary
        public async Task<IDictionary<string, object?>> ParseAsync(string queryString)
        {
            var parser = new DefaultQueryParser();
            return await parser.ParseAsync(queryString, _options.ParserOptions);
        }

        // Method to convert a dictionary to a query string
        public async Task<string> StringifyAsync(IDictionary<string, object?> data)
        {
            var stringifier = new DefaultQueryStringifier();
            return await stringifier.StringifyAsync(data, _options.StringifierOptions);
        }

        // Method to get a configured instance of LimeOptions (useful for inspection)
        public LimeOptions GetOptions() => _options;
    }
}