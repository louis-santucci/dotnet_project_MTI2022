using System;
using System.Collections.Generic;

#nullable disable

namespace FripShop.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }

        public virtual User Buyer { get; set; }
    }
}
