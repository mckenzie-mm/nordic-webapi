using System;
using webapi.Models;

namespace webapi.DTO;

public class ProductPageDTO
{
    public required ProductDTO productDTO { get; init; }
    public required List<ProductDTO> productsDTO { get; init; }

}
