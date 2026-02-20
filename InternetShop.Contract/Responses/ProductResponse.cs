using System.Text.Json.Serialization;

namespace InternetShop.Contract.Responses
{
    public class CategoryBriefResponse
    {
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class ProductResponse
    {
        public long ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public long? ManufacturerId { get; set; }

        public string? ManufacturerName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryBriefResponse>? CategoriesOfProduct { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
