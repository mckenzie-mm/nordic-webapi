using webapi.Domain;

namespace webapi.DTO;

public record class ProductDTOList (List<ProductDTO> list)
{
    public static ProductDTOList fromDomain(List<Product> products)
    {
        var list = new List<ProductDTO>();
        products.ForEach(product =>
        {
            var productResponse = ProductDTO.fromDomain(product);
            list.Add(productResponse);
        });
        return new ProductDTOList(list);
    }
}