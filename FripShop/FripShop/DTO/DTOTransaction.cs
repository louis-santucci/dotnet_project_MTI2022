using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

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
