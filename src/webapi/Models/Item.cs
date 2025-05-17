using System;

namespace webapi.Models;

public class Item
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }
    public required string SmallImage { get; init; }
}
