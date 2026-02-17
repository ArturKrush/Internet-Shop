using InternetShop.Data.Constants;
using System.Text.RegularExpressions;

namespace InternetShop.Data.Entities
{
    public class Manufacturer : BaseEntity
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Manufacturer name can not be empty.");
                }

                // Видалення зайвих пробілів
                string cleanValue = value.Trim();

                // Перевірка, що рядок починається з англійської літери
                if (!Regex.IsMatch(cleanValue, AppConstants.ObjectsNamePattern))
                {
                    throw new ArgumentException("Manufacturer name must starts with an English letter.");
                }

                _name = cleanValue;
            }
        }

        public string? Description { get; set; }

        public DateTime FoundedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
