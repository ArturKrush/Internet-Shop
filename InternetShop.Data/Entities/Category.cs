using System.Text.RegularExpressions;
using InternetShop.Data.Constants;

namespace InternetShop.Data.Entities
{
    public class Category : BaseEntity
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Category name can not be empty.");
                }

                // Видалення зайвих пробілів
                string cleanValue = value.Trim();

                // Перевірка, що рядок починається з англійської літери
                if (!Regex.IsMatch(cleanValue, AppConstants.ObjectsNamePattern))
                {
                    throw new ArgumentException("Category name must starts with an English letter.");
                }

                _name = cleanValue;
            }
        }

        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
