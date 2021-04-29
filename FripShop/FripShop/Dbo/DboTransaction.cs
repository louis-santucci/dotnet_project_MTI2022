﻿using System;
using System.Collections.Generic;
using FripShop.Dbo;

#nullable disable

namespace FripShop.Models.EfModels
{
    public class DboTransaction : IDbo
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
