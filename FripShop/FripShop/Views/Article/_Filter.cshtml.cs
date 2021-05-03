using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.Views.Article
{
    public class _Filter
    {
        public enum Category
        {
            Hauts,
            Bas,
            Chaussures,
            Accessoires
        }
        public enum Comparison
        {
            Date,
            Prix,
            Condition,
            Note
        }

        public string gender { get; set; }
        public string category { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public string conditionMin { get; set; }
        public string sortBy { get; set; }
        public string ascending { get; set; }
        public string search { get; set; }
    }
}
