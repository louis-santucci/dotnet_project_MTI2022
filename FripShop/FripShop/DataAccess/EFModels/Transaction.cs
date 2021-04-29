﻿using System;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }
        public DateTime LastUpdateAt { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Buyer { get; set; }
    }
}
