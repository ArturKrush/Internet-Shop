using InternetShop.Data.Constants;
using System.Text.RegularExpressions;

namespace InternetShop.Data.Entities
{
    public class Customer : BaseEntity
    {
        private string? _firstName;
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name can not be empty.");
                }

                // Видалення зайвих пробілів
                string cleanValue = value.Trim();

                // Перевірка, що рядок починається з англійської літери
                if (!Regex.IsMatch(cleanValue, AppConstants.CustomerNamePattern))
                {
                    throw new ArgumentException("First name must starts with an English letter" +
                        "and contain not more than 2 points in a row.");
                }

                _firstName = cleanValue;
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name can not be empty.");
                }

                // Видалення зайвих пробілів
                string cleanValue = value.Trim();

                // Перевірка, що рядок починається з англійської літери
                if (!Regex.IsMatch(cleanValue, AppConstants.CustomerNamePattern))
                {
                    throw new ArgumentException("First name must starts with an English letter" +
                        "and contain not more than 2 points in a row.");
                }

                _lastName = cleanValue;
            }
        }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
