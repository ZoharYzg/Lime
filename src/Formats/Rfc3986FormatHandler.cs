// src/Formats/Rfc3986FormatHandler.cs
using System;

namespace Lime.Formats
{
    public class Rfc3986FormatHandler : IFormatHandler
    {
        public string Encode(string value)
        {
            return Uri.EscapeDataString(value);
        }
    }
}