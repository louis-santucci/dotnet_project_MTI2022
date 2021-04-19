using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models
{
    public partial class Rating
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long SellerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }

        public virtual User Id1 { get; set; }
        public virtual Article IdNavigation { get; set; }
    }
}
