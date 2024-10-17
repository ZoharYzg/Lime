# Lime
**Lime** Fresh and Zesty Query String Management for .NET

Just like a squeeze of lime adds zest to a dish, Lime brings a fresh approach to managing query strings in .NET. Featuring modern .NET development practices like nullable reference types, async/await, and fluent interfaces for a smooth developer experience.


## Features

- Parse query strings into dictionaries and nested objects.
- Convert dictionaries to query strings with customizable formatting options.
- Support for different array serialization formats (e.g., brackets, indices, repeat, comma).
- Configurable encoding and decoding using RFC3986 or RFC1738 standards.
- Flexible compaction options to clean up data.

## Installation

Install the package from NuGet:

```bash
dotnet add package ZY.Lime --version 1.0.0
```

## Usage

Parsing a Query String

```bash
using Lime.Extensions;

string queryString = "?name=John&age=30&skills[]=CSharp&skills[]=JavaScript";
var parsedData = await queryString.ParseQueryStringAsync();

foreach (var item in parsedData)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}
```

Stringify a Dictionary
```bash
using Lime.Extensions;
using System.Collections.Generic;

var data = new Dictionary<string, object?>
{
    { "name", "John Doe" },
    { "age", 30 },
    { "skills", new List<string> { "CSharp", "JavaScript" } }
};

string queryString = await data.ToQueryStringAsync();
Console.WriteLine(queryString);
```

Configuration
```bash
var options = new LimeOptions()
    .SetFormatHandler(new Rfc1738FormatHandler()) // Use RFC1738 encoding
    .UseParserOptions(new ParserOptions { AllowDots = true })
    .UseStringifierOptions(new StringifierOptions { ArrayFormat = ArrayFormat.Brackets });

var manager = new QueryStringManager(options);
```

## License
This project is licensed under the MIT License.

