using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models.EfModels
{
    public partial class DboTransaction
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }
        public DateTime LastUpdateAt { get; set; }

        public virtual DboArticle Article { get; set; }
        public virtual DboUser Buyer { get; set; }
    }
}
