// src/Parsers/IQueryParser.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lime.Parsers
{
    public interface IQueryParser
    {
        Task<IDictionary<string, object?>> ParseAsync(string queryString, ParserOptions? options = null);
    }
}