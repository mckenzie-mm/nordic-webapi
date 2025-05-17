using System;
using webapi.Models;

namespace webapi.DTO;
public class FormDTO
{
    public required ProductDTO productDTO { get; init; }
    public required List<Category> categories { get; init; }
}
