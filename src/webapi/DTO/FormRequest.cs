using webapi.Domain;

namespace webapi.DTO;

public record FormRequest(
    int id,
    string name,
    string slug,
    string category,
    string description,
    string[] images,
    float price,
    int availability
)
{
    public Product toDomain()
    {
        return new Product
        {
            id = id,
            name = name,
            slug = slug,
            category = category,
            images = String.Join(",", images),
            description = description,
            price = Convert.ToInt32(price * 100),
            availability = availability
        };
    }
}
