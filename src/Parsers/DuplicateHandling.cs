// src/Parsers/DuplicateHandling.cs
namespace Lime.Parsers
{
    public enum DuplicateHandling
    {
        Combine, // Combine values into a list
        First,   // Use the first value encountered
        Last     // Use the last value encountered
    }
}