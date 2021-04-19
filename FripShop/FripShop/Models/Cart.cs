using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models
{
    public partial class Cart
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public int Quantity { get; set; }

        public virtual User Id1 { get; set; }
        public virtual Article IdNavigation { get; set; }
    }
}
