using System;
using webapi.Models;

namespace webapi.DTO;

public class ProductPageDTO
{
    public required Product product { get; init; }
    public required List<Product> similar { get; init; }

}
