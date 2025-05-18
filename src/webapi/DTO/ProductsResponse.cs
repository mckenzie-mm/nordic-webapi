using webapi.Domain;

namespace webapi.DTO;

public record class ProductsResponse (List<ProductResponse> list)
{
    public static ProductsResponse fromDomain(List<Product> products)
    {
        var list = new List<ProductResponse>();
        products.ForEach(product =>
        {
            var productResponse = ProductResponse.fromDomain(product);
            list.Add(productResponse);
        });
        return new ProductsResponse(list);
    }
}