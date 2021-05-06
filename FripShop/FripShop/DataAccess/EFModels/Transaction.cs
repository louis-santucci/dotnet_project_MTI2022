using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    /// <summary>
    /// Class for EntityFrameworkCore transaction model
    /// </summary>
    public class Transaction
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }
        public DateTime LastUpdateAt { get; set; }

        [IgnoreMap]
        public virtual Article Article { get; set; }
        [IgnoreMap]
        public virtual User Buyer { get; set; }
    }
}
