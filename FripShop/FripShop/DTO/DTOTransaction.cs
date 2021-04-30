using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FripShop.DTO;

namespace FripShop.DTO
{
    public class DTOTransaction : IDTO
    {
        [Key]
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public long BuyerId { get; set; }
        public string TransactionState { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
