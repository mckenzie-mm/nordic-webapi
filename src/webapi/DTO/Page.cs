
using webapi.Domain;

namespace webapi.DTO;

public record class Page(
    ProductDTO productDTO,
    ProductDTOList productDTOList
)
{
    public static Page fromDomain(Product product, List<Product> products)
    {
        return new Page(
            ProductDTO.fromDomain(product),
            ProductDTOList.fromDomain(products)
        );
    }
}
