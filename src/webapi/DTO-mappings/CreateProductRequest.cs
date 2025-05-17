using webapi.Models;

namespace webapi.DTO_mappings;

public record CreateProductRequest(
    int id,
    string name,
    string slug,
    string category,
    string description,
    string smallImage,
    float price,
    int availability
)
{
    public Product ToDomain()
    {
        return new Product
        {
            id = id,
            name = name,
            slug = slug,
            category = category,
            smallImage = smallImage,
            mediumImage = smallImage,
            largeImage = smallImage,
            description = description,
            price = Convert.ToInt32(price * 100),
            availability = availability
        };
    }
}
