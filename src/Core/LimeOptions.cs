// src/Core/LimeOptions.cs (Extended)

using System;
using Lime.Formats;

namespace Lime.Core
{
    public class LimeOptions
    {
        // Properties to configure parsing and stringify behavior
        public Parsers.ParserOptions ParserOptions { get; private set; }
        public Stringifiers.StringifierOptions StringifierOptions { get; private set; }
        public IFormatHandler FormatHandler { get; private set; }

        // Constructor initializes with default options
        public LimeOptions()
        {
            ParserOptions = new Parsers.ParserOptions();
            StringifierOptions = new Stringifiers.StringifierOptions();
            FormatHandler = new Rfc3986FormatHandler(); // Default format handler
        }

        // Configuration methods
        public LimeOptions UseParserOptions(Parsers.ParserOptions options)
        {
            ParserOptions = options ?? throw new ArgumentNullException(nameof(options));
            return this;
        }

        public LimeOptions UseStringifierOptions(Stringifiers.StringifierOptions options)
        {
            StringifierOptions = options ?? throw new ArgumentNullException(nameof(options));
            return this;
        }

        public LimeOptions SetFormatHandler(IFormatHandler formatHandler)
        {
            FormatHandler = formatHandler ?? throw new ArgumentNullException(nameof(formatHandler));
            StringifierOptions.Encoder = formatHandler.Encode; // Update the encoder function
            return this;
        }
    }
}