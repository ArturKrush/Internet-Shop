using InternetShop.Data.Constants;
using System.Text.RegularExpressions;

namespace InternetShop.Data.Entities
{
    public class Product : BaseEntity
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Product name can not be empty.");
                }

                // Видалення зайвих пробілів
                string cleanValue = value.Trim();

                // Перевірка, що рядок починається з англійської літери
                if (!Regex.IsMatch(cleanValue, AppConstants.ObjectsNamePattern))
                {
                    throw new ArgumentException("Product name must starts with an English letter.");
                }

                _name = cleanValue;
            }
        }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public long ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = [];

		public virtual ICollection<OrderLine> OrderLines { get; set; } = [];
	}
}
