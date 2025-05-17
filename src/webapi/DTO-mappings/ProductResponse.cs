using webapi.DTO;
using webapi.Models;

namespace webapi.DTO_mappings;

public record ProductResponse(
    Product product,
    List<Category> categories
)
{
    public static FormDTO FromDomain(Product product, List<Category> categories)
    {
        return new FormDTO
        {
            productDTO = Mapping.toProductDTO(product),
            categories = categories
        };
    }
}

