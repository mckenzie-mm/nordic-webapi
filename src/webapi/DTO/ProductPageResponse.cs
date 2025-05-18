
using webapi.Domain;

namespace webapi.DTO;

public record class ProductPageResponse(
    ProductResponse productResponse,
    ProductsResponse productsResponse
)
{
    public static ProductPageResponse fromDomain(Product product, List<Product> products)
    {
        return new ProductPageResponse(
            ProductResponse.fromDomain(product),
            ProductsResponse.fromDomain(products)
        );
    }
}
