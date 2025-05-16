using System;
using webapi.DTO;
using webapi.Models;

namespace webapi.DTO_mappings;

public static class Mapping
{
    public static ProductDTO toProductDTO(Product product)
    {
        return new ProductDTO
        {
            id = product.id,
            name = product.name,
            price = product.price,
            smallImage = product.smallImage.Split(','),
            mediumImage = product.mediumImage.Split(','),
            largeImage = product.largeImage.Split(','),
            slug = product.slug,
            description = product.description,
            availability = product.availability,
            category = product.category
        };
    }

    public static List<ProductDTO> toProductsDTO(List<Product> products)
    {
        var productsDTO = new List<ProductDTO>();
        products.ForEach(p =>
        {
            var productDTO = toProductDTO(p);
            productsDTO.Add(productDTO);
        });
        return productsDTO;
    }
}
