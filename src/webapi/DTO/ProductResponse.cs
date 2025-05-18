
using webapi.Domain;

namespace webapi.DTO;


public record class ProductResponse(
    int id,
    string name,
    int price,
    string[] smallImage,
    string[] mediumImage,
    string[] largeImage,
    string slug,
    string description,
    int availability,
    string category
)
{
    public static ProductResponse fromDomain(Product product)
    {
        return new ProductResponse(
            product.id,
            product.name,
            product.price,
            product.smallImage.Split(','),
            product.mediumImage.Split(','),
            product.largeImage.Split(','),
            product.slug,
            product.description,
            product.availability,
            product.category
        );
    }
}
