// src/Parsers/ParserOptions.cs

using System;

namespace Lime.Parsers
{
    public class ParserOptions
    {
        public static readonly ParserOptions Default = new ParserOptions();

        public string Delimiter { get; set; } = "&";
        public bool IgnoreQueryPrefix { get; set; } = false;
        public bool AllowDots { get; set; } = false;
        public DuplicateHandling DuplicatesHandling { get; set; } = DuplicateHandling.Combine;
        public Func<string, string> Decoder { get; set; } = Uri.UnescapeDataString;
    }
}