using webapi.Domain;

namespace webapi.DTO;

public record FormResponse(
    int id,
    string name,
    int price,
    string[] smallImage,
    string[] mediumImage,
    string[] largeImage, 
    string slug, 
    string description,
    int availability,
    string category,
    List<Category> categories 
)
{
    public static FormResponse fromDomain(Product product, List<Category> categories)
    {
        return new FormResponse (
            product.id,
            product.name,
            product.price,
            product.smallImage.Split(','),
            product.mediumImage.Split(','),
            product.largeImage.Split(','),
            product.slug,
            product.description,
            product.availability,
            product.category,
            categories
        );
    }
}

