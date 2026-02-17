using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands
{
    public class CreateCategoryCommand
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public Category InsertCategory()
        {
            var category = new Category
            {
                Name = Name,
                Description = Description
            };

            return category;
        }
    }
}
