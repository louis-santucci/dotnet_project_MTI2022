using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    /// <summary>
    /// Class for EntityFrameworkCore rating model
    /// </summary>
    public class Rating
    {
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Buyer { get; set; }
    }
}
