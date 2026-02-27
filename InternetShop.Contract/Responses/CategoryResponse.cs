namespace InternetShop.Contract.Responses
{
    public class CategoryResponse
    {
        public long CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<ProductResponse> ProductsOfCategory { get; set; } = [];
    }
}
