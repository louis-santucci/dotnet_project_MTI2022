using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO transaction model
    /// </summary>
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
