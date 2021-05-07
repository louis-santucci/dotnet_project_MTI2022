using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    /// <summary>
    /// Class for EntityFrameworkCore cart model
    /// </summary>
    public class Cart
    {
        public long Id { get; set; }
        public long BuyerId { get; set; }
        public long ArticleId { get; set; }
        public int Quantity { get; set; }

        [IgnoreMap]
        public virtual Article Article { get; set; }
        [IgnoreMap]
        public virtual User Buyer { get; set; }
    }
}
