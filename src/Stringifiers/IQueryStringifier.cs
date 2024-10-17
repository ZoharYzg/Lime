// src/Stringifiers/IQueryStringifier.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lime.Stringifiers
{
    public interface IQueryStringifier
    {
        Task<string> StringifyAsync(IDictionary<string, object?> data, StringifierOptions? options = null);
    }
}