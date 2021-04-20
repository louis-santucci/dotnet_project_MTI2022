using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DTObjects
{
    public class Rating
    {
        public long ArticleId { get; set; }
        public long SellerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Seller { get; set; }
    }
}
