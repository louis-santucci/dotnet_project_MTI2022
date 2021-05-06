using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FripShop.DTO
{
    /// <summary>
    /// Class for DTO article model
    /// </summary>
    public class DTOArticle : IDTO
    {
        [Key]
        public long Id { get; set; }
        public string ImageSource { get; set; }
        public long SellerId { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Sex { get; set; }
        public string Brand { get; set; }
        public int Condition { get; set; }
        public DateTime CreatedAt { get; set; }

        public DTOUserPublic User { get; set; }

        [NotMapped]
        public DTOTransaction Transaction { get; set; }
    }
}
