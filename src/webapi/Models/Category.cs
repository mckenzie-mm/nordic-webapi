using System;

namespace webapi.Models;

public class Category
{
    public int id { get; init; }
    public required string name { get; init; }
    public required string slug { get; init; }
}
