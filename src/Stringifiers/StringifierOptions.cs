// src/Stringifiers/StringifierOptions.cs

using System;

namespace Lime.Stringifiers
{
    public class StringifierOptions
    {
        public static readonly StringifierOptions Default = new StringifierOptions();

        public string Delimiter { get; set; } = "&";
        public bool AddQueryPrefix { get; set; } = false;
        public bool Encode { get; set; } = true;
        public bool SkipNulls { get; set; } = false;
        public bool StrictNullHandling { get; set; } = false;
        public ArrayFormat ArrayFormat { get; set; } = ArrayFormat.Indices;
        public Func<string, string> Encoder { get; set; } = Uri.EscapeDataString;
        public Func<DateTime, string> SerializeDate { get; set; } = date => date.ToString("o"); // ISO format
    }
}