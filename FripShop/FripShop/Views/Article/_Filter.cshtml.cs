using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.Views.Article
{
    public class _Filter
    {   
          public string gender { get; set; }
          public List<string> categories { get; set; }
          public string minPrice { get; set; }
          public string maxPrice { get; set; }
          public string condition { get; set; }
          public string sortBy { get; set; }
          public string ascending { get; set; }
          public string search { get; set; }
    }
}
