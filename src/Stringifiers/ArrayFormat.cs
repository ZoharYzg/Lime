// src/Stringifiers/ArrayFormat.cs
namespace Lime.Stringifiers
{
    public enum ArrayFormat
    {
        Brackets, // e.g., name[]=value1&name[]=value2
        Indices,  // e.g., name[0]=value1&name[1]=value2
        Repeat,   // e.g., name=value1&name=value2
        Comma     // e.g., name=value1,value2
    }
}