
using webapi.Domain;

namespace webapi.DTO;

public record class ProductPage(
    ProductResponse productResponse,
    ProductsResponse productsResponse
)
{
    public static ProductPage fromDomain(Product product, List<Product> products)
    {
        return new ProductPage(
            ProductResponse.fromDomain(product),
            ProductsResponse.fromDomain(products)
        );
    }
}
