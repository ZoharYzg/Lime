// src/Formats/Rfc1738FormatHandler.cs
using System;

namespace Lime.Formats
{
    public class Rfc1738FormatHandler : IFormatHandler
    {
        public string Encode(string value)
        {
            // Encode using RFC3986 first, then replace %20 with +
            return Uri.EscapeDataString(value).Replace("%20", "+");
        }
    }
}