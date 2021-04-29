using System;
using System.Collections.Generic;
using FripShop.Dbo;

namespace FripShop.Models.EfModels
{
    public class DboCart : IDbo
    {
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }

        public virtual DboArticle Article { get; set; }
        public virtual DboUser Buyer { get; set; }
    }
}
