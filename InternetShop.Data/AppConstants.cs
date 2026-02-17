using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Data.Constants
{
    public static class AppConstants
    {
        public const string ObjectsNamePattern = @"^[a-zA-Z]";
        public const string CustomerNamePattern = @"^(?!.*\.\.)[a-zA-Z][a-zA-Z.]*$";
    }
}
