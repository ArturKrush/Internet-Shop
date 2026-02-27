namespace InternetShop.Service.Commands
{
    public class UpdateCategoryCommand
    {
        public long CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
