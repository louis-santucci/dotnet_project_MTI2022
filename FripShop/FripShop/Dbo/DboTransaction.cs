using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FripShop.Dbo;

#nullable disable

namespace FripShop.Dbo
{
    public class DboTransaction : IDbo
    {
        [Key]
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
